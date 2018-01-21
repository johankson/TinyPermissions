using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TinyPermissionsLib.EFCoreProvider
{
    public static class Extensions
    {
        private static List<object> d = new List<object>();

        public static IQueryable<T> WithFunctionPermissions<T>(this DbSet<T> dbset) where T : class
        {
            var key = dbset.GetType().FullName;
            var e = d.OfType<PermissionFunctionFilterEntry<T>>().FirstOrDefault(x => x.DbSetIdentifier == key);

            if (e.FilterQuery != null)
            {
                // This is a generic filter query with a user defined
                return e.FilterQuery(dbset);
            }

            if (e.FilterQueryWithUser != null)
            {
                // This filter query has a user defined with it
                return e.FilterQueryWithUser(dbset, Thread.CurrentPrincipal.Identity);
            }

            // TODO add option if we should throw an exception or allow pass through
            return dbset;
        }

        public static IQueryable<T> WithRole<T>(this DbSet<T> dbset, string role) where T : class
        {
            var key = dbset.GetType().FullName;
            var e = d.OfType<PermissionRoleFilterEntry<T>>().FirstOrDefault(x => x.DbSetIdentifier == key && x.Role == role);

            if (e.FilterQuery != null)
            {
                // This is a generic filter query with a user defined
                return e.FilterQuery(dbset);
            }

            if (e.FilterQueryWithUser != null)
            {
                // This filter query has a user defined with it
                return e.FilterQueryWithUser(dbset, Thread.CurrentPrincipal.Identity);
            }

            return dbset;
        }

        public static void AddPermissionFilter<T>(
            this DbSet<T> dbset,
            string function,
            Func<IQueryable<T>, IQueryable<T>> filterQuery) where T : class
        {
            var entry = new PermissionFunctionFilterEntry<T>()
            {
                DbSetIdentifier = dbset.GetType().FullName,
                Function = function,
                FilterQuery = filterQuery
            };

            d.Add(entry);
        }

        public static void AddFunctionPermissionFilter<T>(
            this DbSet<T> dbset,
            string function,
            Func<IQueryable<T>, IIdentity, IQueryable<T>> filterQuery)
            where T : class
        {
            var entry = new PermissionFunctionFilterEntry<T>()
            {
                DbSetIdentifier = dbset.GetType().FullName,
                Function = function,
                FilterQueryWithUser = filterQuery
            };

            d.Add(entry);
        }

        public static void AddRolePermissionFilter<T>(
            this DbSet<T> dbset,
            string function,
            Func<IQueryable<T>, IIdentity, IQueryable<T>> filterQuery)
            where T : class
        {
            var entry = new PermissionRoleFilterEntry<T>()
            {
                DbSetIdentifier = dbset.GetType().FullName,
                Role = function,
                FilterQueryWithUser = filterQuery
            };

            d.Add(entry);
        }

        public static TinyPermissions UseContext(this TinyPermissions tiny, DbContext context)
        {
            tiny.UserRepository = context as IUserRepository;
            tiny.FunctionRepository = context as IFunctionRepository;
            tiny.RoleRepository = context as IRoleRepository;

            if (tiny.UserRepository == null)
            {
                throw new ArgumentException("The context must implement IUserRepository", nameof(context));
            }

            if (tiny.FunctionRepository == null)
            {
                throw new ArgumentException("The context must implement IFunctionRepository", nameof(context));
            }

            if (tiny.RoleRepository == null)
            {
                throw new ArgumentException("The context must implement IRoleRepository", nameof(context));
            }

            return tiny;
        }

        public static void AddDbContextWithPermissions<T>(
            this IServiceCollection collection,
            Action<DbContextOptionsBuilder> optionsAction = null,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where T : DbContext
        {
            collection.AddDbContext<T>(optionsAction, contextLifetime, optionsLifetime);

            collection.AddSingleton((IServiceProvider arg) =>
            {
                var context = Activator.CreateInstance<T>();
                var tiny = new TinyPermissions().UseContext(context);
                return tiny;
            });
        }
    }
}
