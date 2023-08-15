using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Product : IIdentifiable
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
