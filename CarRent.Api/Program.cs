using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarRent.Api.Data;
using CarRent.Api.Endpoints;
using CarRent.Api.Entities;
using DotNetEnv.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

DotNetEnv.Env.Load();
string secretKey = DotNetEnv.Env.GetString("KEY");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudiences = new[] { "http://localhost:6044", "https://localhost:44391", "http://localhost:5046", "https://localhost:7119" },
        ValidIssuers = new[] { "dotnet-user-jwts" },
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };
    });

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

await app.Services.InitializeDbAsync();
app.MapCarsEndpoints();
app.MapUsersEndpoints();
app.MapReservationsEndpoints();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
