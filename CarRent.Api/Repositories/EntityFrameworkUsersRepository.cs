using CarRent.Api.Data;
using CarRent.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Api.Repositories
{
  public class EntityFrameworkUsersRepository : IUsersRepository
  {
    private readonly CarRentContext dbContext;

    public EntityFrameworkUsersRepository(CarRentContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await dbContext.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
      return await dbContext.Users.FindAsync(id);
    }

    public async Task CreateAsync(User user)
    {
      dbContext.Users.Add(user);
      await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User updatedUser)
    {
      dbContext.Update(updatedUser);
      await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      await dbContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();
    }
  }
}
