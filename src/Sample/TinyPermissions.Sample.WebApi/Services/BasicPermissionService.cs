using System;
using System.Linq;
using TinyPermissionsLib.Sample.Data;
using TinyPermissionsLib.EFCoreProvider;

namespace TinyPermissionsLib.Sample.WebApi.Services
{
    public static class BasicPermissionService
    {
        public static void SetBasicPermissions(DuckContext context)
        {
            context.Ducks.AddRolePermissionFilter(
               "sales",
               (d, u) => d.Where(x => x.Owner.Username == u.Name));
        }
    }
}
