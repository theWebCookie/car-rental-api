using CarRent.Api.Entities;

namespace CarRent.Api.Repositories;

public class InMemCarsRepository : ICarsRepository
{
  private readonly List<Car> cars = new()
    {
      new Car()
      {
        Id = 1,
        Title = "BMW",
        ImageUri = "https://placehold.co/100",
        FuelType = "gas",
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
        FuelType = "gas",
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
        FuelType = "diesel",
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

  public async Task<IEnumerable<Car>> GetAllAsync()
  {
    return await Task.FromResult(cars);
  }

  public async Task<Car?> GetAsync(int id)
  {
    return await Task.FromResult(cars.Find(car => car.Id == id));
  }

  public async Task CreateAsync(Car car)
  {
    car.Id = cars.Max(car => car.Id) + 1;
    cars.Add(car);

    await Task.CompletedTask;
  }

  public async Task UpdateAsync(Car updatedCar)
  {
    var index = cars.FindIndex(car => car.Id == updatedCar.Id);
    cars[index] = updatedCar;

    await Task.CompletedTask;
  }

  public async Task DeleteAsync(int id)
  {
    var index = cars.FindIndex(car => car.Id == id);
    cars.RemoveAt(index);

    await Task.CompletedTask;
  }
}