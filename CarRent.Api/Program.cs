using CarRent.Api.Data;
using CarRent.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

app.Services.InitializeDb();
app.MapCarsEndpoints();
app.Run();
