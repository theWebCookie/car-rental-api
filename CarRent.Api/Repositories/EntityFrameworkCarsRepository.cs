using CarRent.Api.Data;
using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Api.Repositories;

public class EntityFrameworkCarsRepository : ICarsRepository
{
    private readonly CarRentContext dbContext;

    public EntityFrameworkCarsRepository(CarRentContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Car> GetAll()
    {
        return dbContext.Cars.AsNoTracking().ToList();
    }
    public Car? Get(int id)
    {
        return dbContext.Cars.Find(id);
    }

    public void Create(Car car)
    {
        dbContext.Cars.Add(car);
        dbContext.SaveChanges();
    }

    public void Update(Car updatedCar)
    {
        dbContext.Update(updatedCar);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        dbContext.Cars.Where(car => car.Id == id).ExecuteDelete();
    }
}