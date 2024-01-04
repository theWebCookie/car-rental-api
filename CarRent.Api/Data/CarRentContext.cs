using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Api.Data;

public class CarRentContext : DbContext
{
    public CarRentContext(DbContextOptions<CarRentContext> options) : base(options) { }

    public DbSet<Car> Cars => Set<Car>();
}