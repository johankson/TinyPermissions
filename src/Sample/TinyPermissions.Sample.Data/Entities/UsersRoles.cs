using System.ComponentModel.DataAnnotations;

namespace TinyPermissionsLib.Sample.Data
{
    public class UsersRoles
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
