namespace Application.Services.ProductService.DTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrentStock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
