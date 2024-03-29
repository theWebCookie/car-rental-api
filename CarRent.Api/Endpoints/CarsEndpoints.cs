using CarRent.Api.Dtos;
using CarRent.Api.Entities;
using CarRent.Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CarRent.Api.Endpoints;

public static class CarsEndpoints
{
  const string GetCarEndpointName = "GetCarById";
  public static RouteGroupBuilder MapCarsEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/cars").WithParameterValidation();

    group.MapGet("/", async (ICarsRepository repository) => (await repository.GetAllAsync()).Select(car => car.AsDto()));

    group.MapGet("/{id}", async (ICarsRepository repository, int id) =>
    {
      Car? car = await repository.GetAsync(id);
      return car is not null ? Results.Ok(car.AsDto()) : Results.NotFound();
    })
    .WithName(GetCarEndpointName);

    group.MapGet("/type/{carType}", async (ICarsRepository repository, string carType) =>
    {
      var allCars = await repository.GetAllAsync();
      var carsOfType = allCars.Where(car => car.CarType == carType);

      if (carsOfType == null || !carsOfType.Any())
      {
        return Results.NotFound();
      }

      var carDtos = carsOfType.Select(car => car.AsDto());
      return Results.Ok(carDtos);
    });

    group.MapPost("/", async (ICarsRepository repository, CreateCarDto carDto) =>
    {
      Car car = new()
      {
        Title = carDto.Title,
        ImageUri = carDto.ImageUri,
        FuelType = carDto.FuelType,
        Luggage = carDto.Luggage,
        Doors = carDto.Doors,
        Seats = carDto.Seats,
        Transmission = carDto.Transmission,
        FuelUsage = carDto.FuelUsage,
        CarType = carDto.CarType,
        Description = carDto.Description,
        Price = carDto.Price,
        City = carDto.City,
        AvailabilityStart = carDto.AvailabilityStart,
        AvailabilityEnd = carDto.AvailabilityEnd,
      };

      await repository.CreateAsync(car);
      return Results.CreatedAtRoute(GetCarEndpointName, new { id = car.Id }, car);
    })
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    group.MapPut("/updateavailability/{id}", async (ICarsRepository repository, int id, UpdateCarAvailabilityDto updatedAvailabilityDto) =>
    {
      Car? existingCar = await repository.GetAsync(id);
      if (existingCar is null)
      {
        return Results.NotFound();
      }

      existingCar.AvailabilityStart = updatedAvailabilityDto.AvailabilityStart;
      existingCar.AvailabilityEnd = updatedAvailabilityDto.AvailabilityEnd;

      await repository.UpdateAsync(existingCar);
      return Results.NoContent();
    })
    .RequireAuthorization();

    group.MapPut("/{id}", async (ICarsRepository repository, int id, UpdateCarDto updatedCarDto) =>
    {
      Car? existingCar = await repository.GetAsync(id);
      if (existingCar is null)
      {
        return Results.NotFound();
      }

      existingCar.Title = updatedCarDto.Title;
      existingCar.ImageUri = updatedCarDto.ImageUri;
      existingCar.FuelType = updatedCarDto.FuelType;
      existingCar.Luggage = updatedCarDto.Luggage;
      existingCar.Doors = updatedCarDto.Doors;
      existingCar.Seats = updatedCarDto.Seats;
      existingCar.FuelUsage = updatedCarDto.FuelUsage;
      existingCar.CarType = updatedCarDto.CarType;
      existingCar.Transmission = updatedCarDto.Transmission;
      existingCar.Description = updatedCarDto.Description;
      existingCar.Price = updatedCarDto.Price;
      existingCar.City = updatedCarDto.City;
      existingCar.AvailabilityStart = updatedCarDto.AvailabilityStart;
      existingCar.AvailabilityEnd = updatedCarDto.AvailabilityEnd;

      await repository.UpdateAsync(existingCar);
      return Results.NoContent();
    })
    .RequireAuthorization()
    .RequireAuthorization("AdminPolicy");

    group.MapDelete("/{id}", async (ICarsRepository repository, int id) =>
    {
      Car? car = await repository.GetAsync(id);
      if (car is not null)
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