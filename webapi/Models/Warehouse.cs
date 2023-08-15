using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Warehouse : IIdentifiable
    {
        [Key]
        public Guid Id { get; set; }

        public Pharmacy Pharmacy { get; set; }
        [ForeignKey(nameof(Pharmacy))]
        public Guid PharmacyId { get; set; }

        public string Name { get; set; }
    }
}
