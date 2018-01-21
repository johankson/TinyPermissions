using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

namespace TinyPermissionsLib.EFCoreProvider
{
    public static class Extensions
    {
        private static List<object> d = new List<object>();

        public static IQueryable<T> WithPermissions<T>(this DbSet<T> dbset) where T : class
        {
            var key = dbset.GetType().FullName;
            var e = d.OfType<PermissionFilterEntry<T>>().FirstOrDefault(x => x.DbSetIdentifier == key);
           
            var t = e.FilterQuery(dbset);
            return t;
        }

        public static void AddPermissionFilter<T>(this DbSet<T> dbset, 
                                                  string function, 
                                                  Func<IQueryable<T>, IQueryable<T>> filterQuery) where T : class
        {
            var entry = new PermissionFilterEntry<T>()
            {
                DbSetIdentifier = dbset.GetType().FullName,
                Function = function,
                FilterQuery = filterQuery
            };

            d.Add(entry);
        }

        //public static void AddPermissionFilter<T>(this DbSet<T> dbset,
        //                                    string function,
        //                                    Func<IQueryable<T>, IQueryable<T>, GenericIdentity> filterQuery) where T : class
        //{
        //    var entry = new PermissionFilterEntry<T>()
        //    {
        //        DbSetIdentifier = dbset.GetType().FullName,
        //        Function = function,
        //        FilterQuery = filterQuery
        //    };

        //    d.Add(entry);
        //}
    }

    internal class PermissionFilterEntry<T> where T : class
    {
        public string DbSetIdentifier { get; set; }
        public string Function { get; set; }
        public Func<IQueryable<T>, IQueryable<T>> FilterQuery { get; set; }
    }
}
