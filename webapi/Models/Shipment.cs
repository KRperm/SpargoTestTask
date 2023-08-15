using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Shipment : IIdentifiable
    {
        [Key]
        public Guid Id { get; set; }

        public Product Product { get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public Warehouse Warehouse { get; set; }
        [ForeignKey(nameof(Warehouse))]
        public Guid WarehouseId { get; set; }

        public double Amount { get; set; }
    }
}
