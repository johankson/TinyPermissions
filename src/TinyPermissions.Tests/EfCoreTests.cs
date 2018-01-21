using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using TinyPermissionsLib.EFCoreProvider;
using System.Security.Principal;
using System.Threading;

namespace TinyPermissionsLib.Tests
{
    public class EfCoreTests
    {
        [Fact]
        public void EfSetupTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DuckContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new DuckContext(options))
            {
                // Add data
                context.Ducks.Add(new Duck() { Name = "Donald", Size = 42 });
                context.Ducks.Add(new Duck() { Name = "Duffy", Size = 9 });
                context.Functions.Add(new Function() { Id = "get-users", Name = "Get Users", Description = "Gets users" });
                context.Users.Add(new User() { Id = 1, Username = "billybob" });
                context.SaveChanges();

                // Set user information
                var identity = new GenericIdentity("billybob");
                var principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                // Define a permission filter
                context.Ducks.AddPermissionFilter("get-users", (d) => d.Where(s => s.Size > 40));

                // Give billy bob access to the get-users function
                var tiny = new TinyPermissions(context, context);

                tiny.AddFunctionToUser("get-users", "billybob");

                // Make a query (logged in as billybob)
                var data = context.Ducks.WithPermissions();

                Assert.Equal(1, data.Count());
            }

            // Act

            // Assert
        }
    }
}
