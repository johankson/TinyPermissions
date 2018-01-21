namespace TinyPermissionsLib
{
    public interface IFunctionRepository
    {
        IFunction GetFunction(string functionId);
        void AddFunction(IFunction function);
        void AddUserToFunction(IUser user, IFunction function);
        bool UserHasAccessToFunction(IUser user, IFunction function);
    }
}
