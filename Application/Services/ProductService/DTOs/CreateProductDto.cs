namespace Application.Services.ProductService.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrentStock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
