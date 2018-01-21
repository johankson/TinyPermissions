using System;
using System.Linq;

namespace TinyPermissionsLib.Sample.Data
{
    public static class DuckContextSeeder
    {
        public static void Seed(DuckContext context)
        {
            if (context.Users.Any())
            {
                // We already have data in this database
                return;
            }

            var user = new User() { Id = 1, Username = "billybob" };
            var user2 = new User() { Id = 2, Username = "jörgen" };
            context.Users.Add(user);
            context.Users.Add(user2);

            context.Ducks.Add(new Duck() { Name = "Donald", Size = 42, Owner = user });
            context.Ducks.Add(new Duck() { Name = "Duffy", Size = 9, Owner = user2 });
            context.Functions.Add(new Function() { Id = "get-users", Name = "Get Users", Description = "Gets users" });
            context.SaveChanges();
        }
    }
}
