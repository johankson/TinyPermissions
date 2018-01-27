using System;
using Microsoft.EntityFrameworkCore;
using TinyPermissionsLib;
using System.Linq;

namespace TinyPermissionsLib.Sample.Data
{
    public class DuckContext : DbContext, IUserRepository, IRoleRepository
    {
        public DbSet<Duck> Ducks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
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
}
