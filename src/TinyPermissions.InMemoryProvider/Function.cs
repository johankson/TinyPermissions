using System.Collections.Generic;

namespace TinyPermissionsLib.InMemoryProvider
{
    public class Function : IFunction
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<User> Users = new List<User>(); 
    }
}