using System.Reflection;
using CarRent.Api.Data.Configurations;
using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Api.Data;

public class CarRentContext : DbContext
{
    public CarRentContext(DbContextOptions<CarRentContext> options) : base(options) { }

    public DbSet<Car> Cars => Set<Car>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}