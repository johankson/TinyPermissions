namespace TinyPermissionsLib
{
    public interface IUserRepository
    {
        IUser GetUser(string username);
        void AddUser(IUser user);
    }
}
