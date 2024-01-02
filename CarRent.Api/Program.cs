using CarRent.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapCarsEndpoints();
app.Run();
