using System.Collections.Generic;

namespace TinyPermissionsLib.InMemoryProvider
{
    public class Role : IRole
    {
        public string Id { get; set; }

        public List<string> Users { get; set; } = new List<string>();
    }
}