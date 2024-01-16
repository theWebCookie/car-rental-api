using CarRent.Api.Entities;

namespace CarRent.Api.Repositories;

public interface IReservationsRepository
{
  Task CreateAsync(Reservation reservation);
  Task DeleteAsync(int id);
  Task<Reservation?> GetAsync(int id);
  Task<IEnumerable<Reservation>> GetAllAsync();
  Task UpdateAsync(Reservation updatedReservation);
  Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
}