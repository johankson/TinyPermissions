using System;
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
        public void EfRoleTest()
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
                // u = a IIdentity at the moment, describing the current user
                context.Ducks.AddRolePermissionFilter("sales", (d, u) => d.Where(x => x.Owner.Username == u.Name));

                tiny.AddUserToRole("billybob", "sales");

                // Act
                // Make a query (logged in as billybob)
                var data = context.Ducks.WithRole("sales");
                var alldata = context.Ducks;

                // Assert
                Assert.Equal(1, data.Count());
                Assert.Equal(2, alldata.Count());
            }
        }
    }
}
