using CarRent.Api.Entities;

namespace CarRent.Api.Repositories;

public interface ICarsRepository
{
  void Create(Car car);
  void Delete(int id);
  Car? Get(int id);
  IEnumerable<Car> GetAll();
  void Update(Car updatedCar);
}
