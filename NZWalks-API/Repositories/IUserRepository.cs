using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
