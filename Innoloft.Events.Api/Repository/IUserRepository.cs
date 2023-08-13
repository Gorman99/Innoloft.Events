using Innoloft.Events.Api.Data.Entities;

namespace Innoloft.Events.Api.Repository;

public interface IUserRepository
{
    Task<User?> GetUser(int userId);
}