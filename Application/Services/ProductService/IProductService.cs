using Application.Services.ProductService.DTOs;

namespace Application.Services.ProductService
{
    public interface IProductService
    {
        public Task CreateProduct(CreateProductDto input);
        public Task<List<GetAllProductsDto>> GetAllProducts();
        public Task<GetProductByIdDto> GetProductById(Guid id);
        public Task UpdateProduct(Guid id, UpdateProductDto input);
        public Task DeleteProduct(Guid id);
    }
}
