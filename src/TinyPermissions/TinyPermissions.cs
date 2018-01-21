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

    public interface IUserRepository
    {
        IUser GetUser(string username);
        void AddUser(IUser user);
    }

    public interface IFunctionRepository
    {
        IFunction GetFunction(string functionId);
        void AddFunction(IFunction function);
        void AddUserToFunction(IUser user, IFunction function);
        bool UserHasAccessToFunction(IUser user, IFunction function);
    }

    public interface IRoleRepository
    {
        void AddRole(IRole role);
        void AddUserToRole(string username, string role);
        bool HasRole(string username, string role);
    }

    public interface IUser
    {
        string Username { get; set; }
    }

    public interface IFunction
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface IRole
    {
        string Id { get; set; }
    }
}
