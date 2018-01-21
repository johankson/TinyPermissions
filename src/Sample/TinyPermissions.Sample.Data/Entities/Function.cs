using System.ComponentModel.DataAnnotations;

namespace TinyPermissionsLib.Sample.Data
{
    public class Function : IFunction
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
