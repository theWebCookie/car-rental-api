using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Api.Data.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(car => car.Price).HasPrecision(5, 2);
        builder
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Car)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}