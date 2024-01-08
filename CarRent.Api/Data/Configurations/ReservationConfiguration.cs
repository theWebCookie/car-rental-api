using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Api.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
  public void Configure(EntityTypeBuilder<Reservation> builder)
  {
    builder
          .HasOne(r => r.User)
          .WithMany(u => u.Reservations)
          .HasForeignKey(r => r.UserId)
          .OnDelete(DeleteBehavior.Cascade);

    builder
          .HasOne(r => r.Car)
          .WithMany(c => c.Reservations)
          .HasForeignKey(r => r.CarId)
          .OnDelete(DeleteBehavior.Cascade);
  }
}