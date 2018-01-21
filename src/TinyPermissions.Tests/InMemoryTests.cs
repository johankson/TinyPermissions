using System;
using TinyPermissionsLib.InMemoryProvider;
using Xunit;

namespace TinyPermissionsLib.Tests
{
    public class InMemoryTests
    {
        [Fact]
        public void AddFunctionToUserTest()
        {
            // Arrange (with in-memory providers)
            var tiny = new TinyPermissions(new UserRepository(), new FunctionRepository());

            tiny.UserRepository.AddUser(new User() { Username = "johan" });
            tiny.FunctionRepository.AddFunction(new Function() { Id = "get-ducks", Name = "Get ducks" });

            // Act
            tiny.AddFunctionToUser("get-ducks", "johan");

            // Assert
            var result = tiny.HasAccessToFunction("get-ducks", "johan");
            Assert.True(result);
        }
    }
}