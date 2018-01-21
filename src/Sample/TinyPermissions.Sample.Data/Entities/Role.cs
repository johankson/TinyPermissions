using System.ComponentModel.DataAnnotations;

namespace TinyPermissionsLib.Sample.Data
{
    public class Role : IRole
    {
        [Key]
        public string Id { get; set; }
    }
}
