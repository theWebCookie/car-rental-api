using CarRent.Api.Endpoints;
using CarRent.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ICarsRepository, InMemCarsRepository>();

var connString = builder.Configuration.GetConnectionString("CrContext");

var app = builder.Build();

app.MapCarsEndpoints();
app.Run();
