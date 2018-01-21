using System.ComponentModel.DataAnnotations;

namespace TinyPermissionsLib.Sample.Data
{
    public class Duck
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public User Owner { get; set; }
    }
}
