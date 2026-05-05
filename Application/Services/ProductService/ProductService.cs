using Application.Repositories;
using Application.Services.ProductService.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task CreateProduct(CreateProductDto input)
        {
            if (!(await _categoryRepository.GetAll().AnyAsync(c => c.Id == input.CategoryId)))
                throw new Exception("Category not found.");

            if (await _productRepository.GetAll().AnyAsync(x => x.Name == input.Name.ToLower().Trim()))
                throw new Exception("product name already exists.");

            var data = new Product
            {
                Id = Guid.NewGuid(),
                Name = input.Name.ToLower().Trim(),
                SalePrice = input.SalePrice,
                CategoryId = input.CategoryId,
                CurrentStock = 0,
            };

            await _productRepository.InsertAsync(data);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProduct(Guid id)
        {
            var data = await _productRepository.GetByIdAsync(id);

            if (data == null)
                throw new Exception("Product not found.");

            _productRepository.Delete(data);
            await _productRepository.SaveChangesAsync();

        }

        public async Task<List<GetAllProductsDto>> GetAllProducts()
        {
            var data = _productRepository.GetAll().Include(x => x.Category);
            var result = await data.Select(x => new GetAllProductsDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.Category.Name,
            }).ToListAsync();

            return result;
        }

        public async Task<GetProductByIdDto> GetProductById(Guid id)
        {
            var data = await _productRepository.GetAll().Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Product not found.");

            var result = new GetProductByIdDto
            {
                Id = data.Id,
                Name = data.Name,
                CurrentStock = data.CurrentStock,
                SalePrice = data.SalePrice,
                CategoryName = data.Category.Name,
            };

            return result;
        }

        public async Task UpdateProduct(Guid id, UpdateProductDto input)
        {
            if (!(await _categoryRepository.GetAll().AnyAsync(c => c.Id == input.CategoryId)))
                throw new Exception("Category not found.");

            if (await _productRepository.GetAll().AnyAsync(x => x.Name == input.Name.ToLower().Trim() && x.Id != id))
                throw new Exception("product name already exists.");

            var data = await _productRepository.GetByIdAsync(id);

            data.Name = input.Name;
            data.SalePrice = input.SalePrice;
            data.CategoryId = input.CategoryId;

            _productRepository.Update(data);
            await _productRepository.SaveChangesAsync();
        }
    }
}
