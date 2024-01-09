using CarRent.Api.Authorization;
using CarRent.Api.Data;
using CarRent.Api.Endpoints;
using CarRent.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddCarStoreAuthorization();

var app = builder.Build();

await app.Services.InitializeDbAsync();
app.MapCarsEndpoints();
app.MapUsersEndpoints();
app.MapReservationsEndpoints();
app.Run();
