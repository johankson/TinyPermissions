using System.Collections.Generic;

namespace TinyPermissionsLib.InMemoryProvider
{
    public class UserRepository : IUserRepository
    {
        public Dictionary<string, IUser> _users = new Dictionary<string, IUser>();

        public void AddUser(IUser user)
        {
            _users.Add(user.Username, user);
        }

        public IUser GetUser(string username)
        {
            return _users[username.ToLower()];
        }
    }
}