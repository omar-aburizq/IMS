namespace Application.Services.ProductService.DTOs
{
    public class GetProductByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrentStock { get; set; }
        public string CategoryName { get; set; }
    }
}
