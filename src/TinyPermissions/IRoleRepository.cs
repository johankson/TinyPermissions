namespace TinyPermissionsLib
{
    public interface IRoleRepository
    {
        void AddRole(IRole role);
        void AddUserToRole(string username, string role);
        bool HasRole(string username, string role);
    }
}
