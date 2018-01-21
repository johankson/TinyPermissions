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
                var user = new User() { Id = 1, Username = "billybob" };
                var user2 = new User() { Id = 2, Username = "jörgen" };
				context.Users.Add(user);
                context.Ducks.Add(new Duck() { Name = "Donald", Size = 42, Owner = user });
                context.Ducks.Add(new Duck() { Name = "Duffy", Size = 9, Owner = user2 });
                context.Functions.Add(new Function() { Id = "get-users", Name = "Get Users", Description = "Gets users" });
                context.SaveChanges();

                // Set user information
                var identity = new GenericIdentity("billybob");
                var principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                // Define a permission filter, filtering ducks by owner
                // d = us the IQueryable<Duck> filter to add
                // u = a GenericIdentity at the moment, describing the current user
                context.Ducks.AddPermissionFilter("get-users", (d, u) => d.Where(x => x.Owner.Username == u.Name));

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
