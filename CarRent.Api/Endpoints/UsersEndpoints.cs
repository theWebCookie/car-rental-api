using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarRent.Api.Dtos;
using CarRent.Api.Entities;
using CarRent.Api.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace CarRent.Api.Endpoints;

public static class UsersEndpoints
{
  const string GetUserEndpointName = "GetUserById";

  public static RouteGroupBuilder MapUsersEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/users").WithParameterValidation();

    group.MapGet("/", async (IUsersRepository repository) => (await repository.GetAllAsync()).Select(user => user.AsDto())).RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    group.MapGet("/{id}", async (IUsersRepository repository, int id, ClaimsPrincipal user) =>
    {
      var requestingUserIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

      if (requestingUserIdClaim == null || !int.TryParse(requestingUserIdClaim.Value, out int requestingUserId))
      {
        return Results.Unauthorized(); ;
      }

      if (requestingUserId != id)
      {
        return Results.Unauthorized();
      }

      var authorizedUser = await repository.GetAsync(id);

      if (authorizedUser == null)
      {
        return Results.NotFound();
      }
      return Results.Ok(authorizedUser.AsDto());
    })
    .WithName(GetUserEndpointName)
    .RequireAuthorization();

    group.MapPost("/", async (IUsersRepository repository, CreateUserDto userDto) =>
    {
      var existingUser = await repository.GetByEmailAsync(userDto.Email);

      if (existingUser != null)
      {
        return Results.Conflict(new { message = "User with the given email already exists." });
      }

      if (string.IsNullOrEmpty(userDto.FirstName) || string.IsNullOrEmpty(userDto.SeccondName))
      {
        return Results.BadRequest(new { message = "Invalid name!" });
      }

      if (string.IsNullOrEmpty(userDto.Email) || !userDto.Email.Contains('@'))
      {
        return Results.BadRequest(new { message = "Invalid email!" });
      }

      if (string.IsNullOrEmpty(userDto.Password) || userDto.Password.Trim().Length < 7)
      {
        return Results.BadRequest(new { message = "Invalid input - password should be at least 7 characters long!" });
      }

      User user = new()
      {
        FirstName = userDto.FirstName,
        SeccondName = userDto.SeccondName,
        Email = userDto.Email,
        Password = userDto.Password,
        Role = userDto.Role
      };

      await repository.CreateAsync(user);

      var tokenHandler = new JwtSecurityTokenHandler();
      DotNetEnv.Env.Load();
      string secretKey = DotNetEnv.Env.GetString("KEY");
      var key = Encoding.UTF8.GetBytes(secretKey);

      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, user.Id.ToString()),
        new Claim("aud", "http://localhost:6044"),
        new Claim("aud", "https://localhost:44391"),
        new Claim("aud", "http://localhost:5046"),
        new Claim("aud", "https://localhost:7119"),
        new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
        new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddHours(1)).ToUnixTimeSeconds().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer),
        new Claim(JwtRegisteredClaimNames.Iss, "dotnet-user-jwts")
      };

      if (!string.IsNullOrEmpty(userDto.Role) && userDto.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
      {
        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
      }

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return Results.Ok(new { userId = user.Id, token = tokenString });
    });

    group.MapPost("/login", async (IUsersRepository repository, LoginDto loginDto) =>
    {
      var user = await repository.GetByEmailAsync(loginDto.Email);

      if (user != null && user.Password == loginDto.Password)
      {
        var tokenHandler = new JwtSecurityTokenHandler();
        DotNetEnv.Env.Load();
        string secretKey = DotNetEnv.Env.GetString("KEY");
        var key = Encoding.UTF8.GetBytes(secretKey);

        var claims = new List<Claim>
        {
          new Claim(ClaimTypes.Name, user.Id.ToString()),
          new Claim("aud", "http://localhost:6044"),
          new Claim("aud", "https://localhost:44391"),
          new Claim("aud", "http://localhost:5046"),
          new Claim("aud", "https://localhost:7119"),
          new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
          new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddHours(1)).ToUnixTimeSeconds().ToString()),
          new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer),
          new Claim(JwtRegisteredClaimNames.Iss, "dotnet-user-jwts")
        };

        if (!string.IsNullOrEmpty(user.Role) && user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
          claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.UtcNow.AddHours(1),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Results.Ok(new { userId = user.Id, token = tokenString });
      }

      return Results.BadRequest(new { message = "Invalid email or password." });
    });

    group.MapPut("/{id}", async (IUsersRepository repository, int id, UpdateUserDto updatedUserDto) =>
    {
      User? existingUser = await repository.GetAsync(id);
      if (existingUser is null)
      {
        return Results.NotFound();
      }

      existingUser.FirstName = updatedUserDto.FirstName;
      existingUser.SeccondName = updatedUserDto.SeccondName;
      existingUser.Email = updatedUserDto.Email;
      existingUser.Password = updatedUserDto.Password;
      existingUser.Role = updatedUserDto.Role;

      await repository.UpdateAsync(existingUser);
      return Results.NoContent();
    })
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    group.MapDelete("/{id}", async (IUsersRepository repository, int id) =>
    {
      User? user = await repository.GetAsync(id);
      if (user is not null)
      {
        await repository.DeleteAsync(id);
      }

      return Results.NoContent();
    })
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    return group;
  }
}