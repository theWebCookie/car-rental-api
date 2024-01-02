using CarRent.Api.Entities;

namespace CarRent.Api.Endpoints;

public static class CarsEndpoints
{
  const string GetCarEndpointName = "GetCarById";
  static List<Car> cars = new()
  {
    new Car()
    {
      Id = 1,
      Title = "BMW",
      ImageUri = "https://placehold.co/100",
      Fuel = 5.2,
      Luggage = 30,
      Doors = 5,
      Seats = 5,
      FuelUsage = 6.7,
      CarType = "car",
      Transmission = "automat",
      Description = "aaaaaaaaaaaaaaaaaaaaaaaaa",
      Price = 200,
    },
      new Car()
    {
      Id = 2,
      Title = "AUDI",
      ImageUri = "https://placehold.co/100",
      Fuel = 6.2,
      Luggage = 35,
      Doors = 3,
      Seats = 2,
      FuelUsage = 12.7,
      CarType = "car",
      Transmission = "automat",
      Description = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbb",
      Price = 500,
    },
      new Car()
    {
      Id = 3,
      Title = "Mercedes",
      ImageUri = "https://placehold.co/100",
      Fuel = 3.2,
      Luggage = 20,
      Doors = 3,
      Seats = 4,
      FuelUsage = 5.7,
      CarType = "car",
      Transmission = "automat",
      Description = "ccccccccccccccccccccccccccccc",
      Price = 150,
    }
  };
  public static RouteGroupBuilder MapCarsEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/cars").WithParameterValidation();

    group.MapGet("/", () => cars);

    group.MapGet("/{id}", (int id) =>
    {
      Car? car = cars.Find(car => car.Id == id);
      if (car is null)
      {
        return Results.NotFound();
      }
      return Results.Ok(car);
    })
    .WithName(GetCarEndpointName);

    group.MapPost("/", (Car car) =>
    {
      car.Id = cars.Max(car => car.Id) + 1;
      cars.Add(car);
      return Results.CreatedAtRoute(GetCarEndpointName, new { id = car.Id }, car);
    });

    group.MapPut("/{id}", (int id, Car updatedCar) =>
    {
      Car? existingCar = cars.Find(car => car.Id == id);
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

      return Results.NoContent();
    });

    group.MapDelete("/{id}", (int id) =>
    {
      Car? car = cars.Find(car => car.Id == id);
      if (car is not null)
      {
        cars.Remove(car);
      }

      return Results.NoContent();
    });
    return group;
  }
}