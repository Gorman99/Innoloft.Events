using Innoloft.Events.Api.Data.Context;
using Innoloft.Events.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Innoloft.Events.Api.Repository;

public class UserRepository : IUserRepository
{
    private readonly EventsDbContext _dbContext;

    public UserRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUser(int userId)
    {
        return await _dbContext.Users.Include( d => d.Company).FirstOrDefaultAsync(u => u.Id.Equals(userId));
    }
}