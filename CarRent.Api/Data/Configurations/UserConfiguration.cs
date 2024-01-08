using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Api.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder
          .HasMany(u => u.Reservations)
          .WithOne(r => r.User)
          .HasForeignKey(r => r.UserId)
          .OnDelete(DeleteBehavior.Cascade);
  }
}