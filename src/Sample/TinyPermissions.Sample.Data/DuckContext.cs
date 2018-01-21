using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TinyPermissionsLib;
using System.Linq;

namespace TinyPermissionsLib.Sample.Data
{
    public class DuckContext : DbContext, IUserRepository, IFunctionRepository, IRoleRepository
    {
        public DbSet<Duck> Ducks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<UsersFunctions> UsersFunctions { get; set; }
        public DbSet<UsersRoles> UsersRoles { get; set; }

        public DuckContext()
        {
        }

        public DuckContext(DbContextOptions<DuckContext> options) : base(options)
        {
        }

        public IUser GetUser(string username)
        {
            var user = Users.FirstOrDefault(x => x.Username == username);
            return user;
        }

        public void AddUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public IFunction GetFunction(string functionId)
        {
            var function = Functions.FirstOrDefault(x => x.Id == functionId);
            return function;
        }

        public void AddFunction(IFunction function)
        {
            throw new NotImplementedException();
        }

        public void AddUserToFunction(IUser user, IFunction function)
        {
            var item = new UsersFunctions()
            {
                User = GetUser(user.Username) as User,
                Function = GetFunction(function.Id) as Function
            };

            UsersFunctions.Add(item);
            SaveChanges();
        }

        public bool UserHasAccessToFunction(IUser user, IFunction function)
        {
            throw new NotImplementedException();
        }

        public void AddRole(IRole role)
        {
            var item = new Role()
            {
                Id = role.Id
            };

            Roles.Add(item);
            SaveChanges();
        }

        public void AddUserToRole(string username, string role)
        {
            var item = new UsersRoles()
            {
                Role = Roles.First(e => e.Id == role),
                User = Users.First(e => e.Username == username)
            };
            UsersRoles.Add(item);
            SaveChanges(); 
        }

        public bool HasRole(string username, string role)
        {
            throw new NotImplementedException();
        }
    }

    public class Duck
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public User Owner { get; set; }
    }

    public class User : IUser
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
    }

    public class UsersFunctions
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public Function Function { get; set; }
    }

    public class UsersRoles
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }

    public class Function : IFunction
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class Role : IRole
    {
        [Key]
        public string Id { get; set; }
    }
}
