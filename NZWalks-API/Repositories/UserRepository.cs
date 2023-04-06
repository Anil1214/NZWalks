using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public UserRepository(NZWalksDbContext nZWalksDbContext) 
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await nZWalksDbContext.Users.
                FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            if(user == null)
            {
                return null;
            }
            var userRoles = await nZWalksDbContext.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var role in userRoles)
                {
                    var userRole = await nZWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == role.RoleId);
                    if(userRole != null)
                    {
                        user.Roles.Add(userRole.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}
