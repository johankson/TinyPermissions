using System;
namespace TinyPermissionsLib
{
    public class TinyPermissions
    {
        public TinyPermissions()
        {
        }

        public TinyPermissions(IUserRepository userRepository, 
                               IRoleRepository roleRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        public bool HasRole(string username, string role)
        {
            return RoleRepository.HasRole(username, role);
        }

        public void AddUserToRole(string username, string role)
        {
            RoleRepository.AddUserToRole(username, role); 
        }

        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
    }
}
