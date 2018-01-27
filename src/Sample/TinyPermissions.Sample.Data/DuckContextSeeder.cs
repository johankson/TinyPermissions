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

            var user = new User() { Username = "billybob" };
            var user2 = new User() { Username = "jörgen" };
            context.Users.Add(user);
            context.Users.Add(user2);
            context.SaveChanges();

            context.Roles.Add(new Role() { Id = "sales" });

            context.Ducks.Add(new Duck() { Name = "Donald", Size = 42, Owner = user });
            context.Ducks.Add(new Duck() { Name = "Duffy", Size = 9, Owner = user2 });
            context.SaveChanges();
        }
    }
}
