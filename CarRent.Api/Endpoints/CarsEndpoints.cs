using CarRent.Api.Dtos;
using CarRent.Api.Entities;
using CarRent.Api.Repositories;

namespace CarRent.Api.Endpoints;

public static class CarsEndpoints
{
  const string GetCarEndpointName = "GetCarById";
  public static RouteGroupBuilder MapCarsEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/cars").WithParameterValidation();

    group.MapGet("/", (ICarsRepository repository) => repository.GetAll().Select(car => car.AsDto()));

    group.MapGet("/{id}", (ICarsRepository repository, int id) =>
    {
      Car? car = repository.Get(id);
      return car is not null ? Results.Ok(car.AsDto()) : Results.NotFound();
    })
    .WithName(GetCarEndpointName);

    group.MapPost("/", (ICarsRepository repository, CreateCarDto carDto) =>
    {
      Car car = new()
      {
        Title = carDto.Title,
        ImageUri = carDto.ImageUri,
        Fuel = carDto.Fuel,
        Luggage = carDto.Luggage,
        Doors = carDto.Doors,
        Seats = carDto.Seats,
        Transmission = carDto.Transmission,
        FuelUsage = carDto.FuelUsage,
        CarType = carDto.CarType,
        Description = carDto.Description,
        Price = carDto.Price,
      };

      repository.Create(car);
      return Results.CreatedAtRoute(GetCarEndpointName, new { id = car.Id }, car);
    });

    group.MapPut("/{id}", (ICarsRepository repository, int id, UpdateCarDto updatedCarDto) =>
    {
      Car? existingCar = repository.Get(id);
      if (existingCar is null)
      {
        return Results.NotFound();
      }

      existingCar.Title = updatedCarDto.Title;
      existingCar.ImageUri = updatedCarDto.ImageUri;
      existingCar.Fuel = updatedCarDto.Fuel;
      existingCar.Luggage = updatedCarDto.Luggage;
      existingCar.Doors = updatedCarDto.Doors;
      existingCar.Seats = updatedCarDto.Seats;
      existingCar.FuelUsage = updatedCarDto.FuelUsage;
      existingCar.CarType = updatedCarDto.CarType;
      existingCar.Transmission = updatedCarDto.Transmission;
      existingCar.Description = updatedCarDto.Description;
      existingCar.Price = updatedCarDto.Price;

      repository.Update(existingCar);
      return Results.NoContent();
    });

    group.MapDelete("/{id}", (ICarsRepository repository, int id) =>
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