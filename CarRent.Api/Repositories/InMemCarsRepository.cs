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

  public IEnumerable<Car> GetAll()
  {
    return cars;
  }

  public Car? Get(int id)
  {
    return cars.Find(car => car.Id == id);
  }

  public void Create(Car car)
  {
    car.Id = cars.Max(car => car.Id) + 1;
    cars.Add(car);
  }

  public void Update(Car updatedCar)
  {
    var index = cars.FindIndex(car => car.Id == updatedCar.Id);
    cars[index] = updatedCar;
  }

  public void Delete(int id)
  {
    var index = cars.FindIndex(car => car.Id == id);
    cars.RemoveAt(index);
  }
}