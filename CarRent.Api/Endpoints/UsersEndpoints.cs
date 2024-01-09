using CarRent.Api.Authorization;
using CarRent.Api.Dtos;
using CarRent.Api.Entities;
using CarRent.Api.Repositories;

namespace CarRent.Api.Endpoints;

public static class UsersEndpoints
{
  const string GetUserEndpointName = "GetUserById";
  public static RouteGroupBuilder MapUsersEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/users").WithParameterValidation();

    group.MapGet("/", async (IUsersRepository repository) => (await repository.GetAllAsync()).Select(user => user.AsDto()));

    group.MapGet("/{id}", async (IUsersRepository repository, int id) =>
    {
      User? user = await repository.GetAsync(id);
      return user is not null ? Results.Ok(user.AsDto()) : Results.NotFound();
    })
    .WithName(GetUserEndpointName)
    .RequireAuthorization(Policies.ReadAccess);

    group.MapPost("/", async (IUsersRepository repository, CreateUserDto userDto) =>
    {
      User user = new()
      {
        FirstName = userDto.FirstName,
        SeccondName = userDto.SeccondName,
        Email = userDto.Email,
        Password = userDto.Password
      };

      await repository.CreateAsync(user);
      return Results.CreatedAtRoute(GetUserEndpointName, new { id = user.Id }, user);
    })
    .RequireAuthorization(Policies.WriteAccess);

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

      await repository.UpdateAsync(existingUser);
      return Results.NoContent();
    })
    .RequireAuthorization(Policies.WriteAccess);

    group.MapDelete("/{id}", async (IUsersRepository repository, int id) =>
    {
      User? user = await repository.GetAsync(id);
      if (user is not null)
      {
        await repository.DeleteAsync(id);
      }

      return Results.NoContent();
    })
    .RequireAuthorization(policy =>
    {
      policy.RequireRole("Admin");
    });

    return group;
  }
}