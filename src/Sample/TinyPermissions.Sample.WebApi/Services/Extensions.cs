using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TinyPermissionsLib.EFCoreProvider;

namespace TinyPermissionsLib.Sample.WebApi.Services
{
    public static class Extensions
    {
        //public static void AddDbContextWithPermissions<T>(
        //    this IServiceCollection collection, 
        //    Action<Microsoft.EntityFrameworkCore.DbContextOptionsBuilder> optionsAction = null,
        //    ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        //    ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where T : DbContext
        //{
        //    collection.AddDbContext<T>(optionsAction, contextLifetime, optionsLifetime);

        //    collection.AddSingleton((IServiceProvider arg) =>
        //    {
        //        var context = Activator.CreateInstance<T>();
        //        var tiny = new TinyPermissions().UseContext(context);
        //        return tiny;
        //    });
        //}
    }
}
