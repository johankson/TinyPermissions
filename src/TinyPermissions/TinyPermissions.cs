using System;
namespace TinyPermissionsLib
{
    public class TinyPermissions
    {
        public TinyPermissions(IUserRepository userRepository, IFunctionRepository functionRepository)
        {
            UserRepository = userRepository;
            FunctionRepository = functionRepository;
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

        public IUserRepository UserRepository { get; private set; }
        public IFunctionRepository FunctionRepository { get; private set; }
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
}
