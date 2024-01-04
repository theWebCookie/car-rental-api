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

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await dbContext.Cars.AsNoTracking().ToListAsync();
    }
    public async Task<Car?> GetAsync(int id)
    {
        return await dbContext.Cars.FindAsync(id);
    }

    public async Task CreateAsync(Car car)
    {
        dbContext.Cars.Add(car);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Car updatedCar)
    {
        dbContext.Update(updatedCar);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Cars.Where(car => car.Id == id).ExecuteDeleteAsync();
    }
}