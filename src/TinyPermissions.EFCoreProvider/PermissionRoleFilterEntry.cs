using System;
using System.Linq;
using System.Security.Principal;

namespace TinyPermissionsLib.EFCoreProvider
{
    internal class PermissionRoleFilterEntry<T> where T : class
    {
        public string DbSetIdentifier { get; internal set; }
        public string Role { get; internal set; }
        public Func<IQueryable<T>, IQueryable<T>> FilterQuery { get; internal set; }
        public Func<IQueryable<T>, IIdentity, IQueryable<T>> FilterQueryWithUser { get; internal set; }
    }
}
