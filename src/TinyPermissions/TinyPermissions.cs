using System;
namespace TinyPermissionsLib
{
    public class TinyPermissions
    {
        public TinyPermissions()
        {
        }

        public TinyPermissions(IUserRepository userRepository, 
                               IFunctionRepository functionRepository,
                               IRoleRepository roleRepository)
        {
            UserRepository = userRepository;
            FunctionRepository = functionRepository;
            RoleRepository = roleRepository;
        }

        public void AddFunction(IFunction function)
        {
            FunctionRepository.AddFunction(function);
        }

        public void AddFunctionToUser(string functionIdentifier, string username)
        {
            var f = FunctionRepository.GetFunction(functionIdentifier);
            var u = UserRepository.GetUser(username); 

            FunctionRepository.AddUserToFunction(u, f);
        }

        public bool HasAccessToFunction(string functionIdentifier, string username)
        {
            var f = FunctionRepository.GetFunction(functionIdentifier);
            var u = UserRepository.GetUser(username);

            return FunctionRepository.UserHasAccessToFunction(u, f);
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
        public IFunctionRepository FunctionRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
    }
}
