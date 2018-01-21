using System.Collections.Generic;
using System.Linq;

namespace TinyPermissionsLib.InMemoryProvider
{
    public class RoleRepository : IRoleRepository
    {
        private List<Role> _roles = new List<Role>();

        public void AddRole(IRole role)
        {
            _roles.Add(new Role() { Id = role.Id });
        }

        public void AddUserToRole(string username, string role)
        {
            var r = _roles.FirstOrDefault(x => x.Id == role);
            r.Users.Add(username);
        }

        public bool HasRole(string username, string role)
        {
            var r = _roles.FirstOrDefault(x => x.Id == role);
            return r.Users.Any(x => x == username);
        }
    }
}