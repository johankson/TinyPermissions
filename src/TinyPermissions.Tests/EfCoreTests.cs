﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using TinyPermissionsLib.EFCoreProvider;
using System.Security.Principal;
using System.Threading;
using TinyPermissionsLib.Sample.Data;

namespace TinyPermissionsLib.Tests
{
    public class EfCoreTests
    {
        [Fact]
        public void EfSetupTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DuckContext>()
                .UseInMemoryDatabase(databaseName: "test_context")
                .Options;

            // Run the test against one instance of the context
            using (var context = new DuckContext(options))
            {
                // Add data
                DuckContextSeeder.Seed(context);

                // Set user information
                var identity = new GenericIdentity("billybob");
                var principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                // Give billy bob access to the get-users function
                var tiny = new TinyPermissions().UseContext(context);

                // Define a permission filter, filtering ducks by owner
                // d = us the IQueryable<Duck> filter to add
                // u = a GenericIdentity at the moment, describing the current user
                context.Ducks.AddPermissionFilter("get-users", (d, u) => d.Where(x => x.Owner.Username == u.Name));

                tiny.AddFunctionToUser("get-users", "billybob");

                // Make a query (logged in as billybob)
                var data = context.Ducks.WithPermissions();
                var alldata = context.Ducks;

                Assert.Equal(1, data.Count());
                Assert.Equal(2, alldata.Count());

                // Add permission filter
                //context.Ducks.AddPermissionFilter(Role.Gamer, (d, u) => d.Where(x => x.Owner.Username == u.Name));

                //// Query using the permission filter
                //data = context.Ducks.WithPermissions(Role.Gamer);
            }

            // Act

            // Assert
        }
    }

    public class Role
    {
        public static string Gamer = "Gamer";
    }
}
