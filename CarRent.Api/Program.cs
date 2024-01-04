using CarRent.Api.Data;
using CarRent.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

await app.Services.InitializeDbAsync();
app.MapCarsEndpoints();
app.Run();
