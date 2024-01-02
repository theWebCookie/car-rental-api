using CarRent.Api.Entities;
using CarRent.Api.Repositories;

namespace CarRent.Api.Endpoints;

public static class CarsEndpoints
{
  const string GetCarEndpointName = "GetCarById";
  public static RouteGroupBuilder MapCarsEndpoints(this IEndpointRouteBuilder routes)
  {
    InMemCarsRepository repository = new();

    var group = routes.MapGroup("/cars").WithParameterValidation();

    group.MapGet("/", () => repository.GetAll());

    group.MapGet("/{id}", (int id) =>
    {
      Car? car = repository.Get(id);
      return car is not null ? Results.Ok(car) : Results.NotFound();
    })
    .WithName(GetCarEndpointName);

    group.MapPost("/", (Car car) =>
    {
      repository.Create(car);
      return Results.CreatedAtRoute(GetCarEndpointName, new { id = car.Id }, car);
    });

    group.MapPut("/{id}", (int id, Car updatedCar) =>
    {
      Car? existingCar = repository.Get(id);
      if (existingCar is null)
      {
        return Results.NotFound();
      }

      existingCar.Title = updatedCar.Title;
      existingCar.ImageUri = updatedCar.ImageUri;
      existingCar.Fuel = updatedCar.Fuel;
      existingCar.Luggage = updatedCar.Luggage;
      existingCar.Doors = updatedCar.Doors;
      existingCar.Seats = updatedCar.Seats;
      existingCar.FuelUsage = updatedCar.FuelUsage;
      existingCar.CarType = updatedCar.CarType;
      existingCar.Transmission = updatedCar.Transmission;
      existingCar.Description = updatedCar.Description;
      existingCar.Price = updatedCar.Price;

      repository.Update(existingCar);
      return Results.NoContent();
    });

    group.MapDelete("/{id}", (int id) =>
    {
      Car? car = repository.Get(id);
      if (car is not null)
      {
        repository.Delete(id);
      }

      return Results.NoContent();
    });
    return group;
  }
}