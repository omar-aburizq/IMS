namespace Application.Services.ProductService.DTOs
{
    public class GetAllProductsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}
