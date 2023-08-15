namespace WebApi.Dto.Product
{
    public class StockDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double TotalAmount { get; set; }
    }
}
