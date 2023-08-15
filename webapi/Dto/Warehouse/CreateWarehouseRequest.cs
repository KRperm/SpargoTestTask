namespace WebApi.Dto.Warehouse
{
    public class CreateWarehouseRequest
    {
        public Guid PharmacyId { get; set; }
        public string Name { get; set; }
    }
}
