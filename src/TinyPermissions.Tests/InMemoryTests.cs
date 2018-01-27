using System;
using TinyPermissionsLib.InMemoryProvider;
using Xunit;

namespace TinyPermissionsLib.Tests
{
    public class InMemoryTests
    {
        [Fact]
        public void AddUserToRoleTest()
        {
            // Arrange
            var tiny = new TinyPermissions(new UserRepository(), new RoleRepository());
            tiny.UserRepository.AddUser(new User() { Username = "johan" });
            tiny.RoleRepository.AddRole(new Role() { Id = "administrators" });

            // Act
            tiny.AddUserToRole("johan", "administrators");

            // Assert
            var result = tiny.HasRole("johan", "administrators");
        }
    }
}
