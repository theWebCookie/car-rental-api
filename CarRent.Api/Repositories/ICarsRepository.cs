using CarRent.Api.Entities;

namespace CarRent.Api.Repositories;

public interface ICarsRepository
{
  Task CreateAsync(Car car);
  Task DeleteAsync(int id);
  Task<Car?> GetAsync(int id);
  Task<IEnumerable<Car>> GetAllAsync();
  Task UpdateAsync(Car updatedCar);
}
