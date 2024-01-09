using CarRent.Api.Data;
using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Api.Repositories
{
  public class EntityFrameworkReservationsRepository : IReservationsRepository
  {
    private readonly CarRentContext dbContext;

    public EntityFrameworkReservationsRepository(CarRentContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
      return await dbContext.Reservations.AsNoTracking().ToListAsync();
    }

    public async Task<Reservation?> GetAsync(int id)
    {
      return await dbContext.Reservations.FindAsync(id);
    }

    public async Task CreateAsync(Reservation reservation)
    {
      dbContext.Reservations.Add(reservation);
      await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Reservation updatedReservation)
    {
      dbContext.Update(updatedReservation);
      await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      await dbContext.Reservations.Where(reservation => reservation.Id == id).ExecuteDeleteAsync();
    }
  }
}
