using Application.Services.ProductService.DTOs;

namespace Application.Services.ProductService
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductDto input);
        Task<List<GetAllProductsDto>> GetAllProducts();
        Task<GetProductByIdDto> GetProductById(Guid id);
        Task UpdateProduct(Guid id, UpdateProductDto input);
        Task DeleteProduct(Guid id);
    }
}
