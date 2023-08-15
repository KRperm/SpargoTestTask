namespace WebApi.Dto.Shipment
{
    public class ShipmentDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public double Amount { get; set; }
    }
}
