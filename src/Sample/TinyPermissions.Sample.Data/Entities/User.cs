using System.ComponentModel.DataAnnotations;

namespace TinyPermissionsLib.Sample.Data
{
    public class User : IUser
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
