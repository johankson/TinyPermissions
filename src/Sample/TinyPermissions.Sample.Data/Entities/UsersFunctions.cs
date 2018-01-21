using System.ComponentModel.DataAnnotations;

namespace TinyPermissionsLib.Sample.Data
{
    public class UsersFunctions
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public Function Function { get; set; }
    }
}
