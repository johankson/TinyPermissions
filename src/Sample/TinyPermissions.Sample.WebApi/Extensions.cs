using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TinyPermissionsLib.Sample.Data;

namespace TinyPermissionsLib.Sample.WebApi
{
    public static class DbStorageExtensions
    {
        public static IWebHost SeedDb(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbcontext = services.GetService<DuckContext>();
                DuckContextSeeder.Seed(dbcontext);
            }

            return host;
        }

       
    }
}
