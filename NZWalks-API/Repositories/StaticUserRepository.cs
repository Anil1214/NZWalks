using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> _users = new List<User>()
        {
            //new User()
            //{
            //    Firstname = "Read",Lastname="only", Password="1234",Email="readonly@user.com", Id= new Guid(), Username = "Readonly@user.com",Roles = new List<string>(){ "reader"}
            //},
            //new User()
            //{
            //    Firstname = "Read",Lastname="write", Password="12345",Email="readwrite@user.com", Id= new Guid(), Username = "ReadWrite@user.com",Roles = new List<string>(){ "reader", "writer"}
            //}
        };
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = _users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);
            return user;
        }
    }
}
