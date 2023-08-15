namespace WebApi.Dto.Shipment
{
    public class CreateShipmentRequest
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public double Amount { get; set; }
    }
}
