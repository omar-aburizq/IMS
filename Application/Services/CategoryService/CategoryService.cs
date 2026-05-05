using Application.Repositories;
using Application.Services.CategoryService.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategory(CreateCategoryDto input)
        {
            if (await _categoryRepository.GetAll().AnyAsync(x => x.Name == input.Name.ToLower().Trim()))
                throw new Exception("category name already exists.");

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = input.Name.ToLower().Trim(),
                Description = input.Description,
                Sort = input.Sort,
            };

            await _categoryRepository.InsertAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid id)
        {
            var data = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Category was not found.");

            _categoryRepository.Delete(data);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<List<GetAllCategoriesDto>> GetAllCategories()
        {
            var data = await _categoryRepository.GetAll().Select(x => new GetAllCategoriesDto
            {
                Id = x.Id,
                Name = x.Name,
                Sort = x.Sort,
            }).OrderBy(x => x.Sort).ToListAsync();

            return data;
        }

        public async Task<GetCategoryByIdDto> GetCategoryById(Guid id)
        {
            var data = await _categoryRepository.GetByIdAsync(id);

            if (data == null)
                throw new Exception("Category was not found.");

            var result = new GetCategoryByIdDto
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Sort = data.Sort,
            };
            return result;
        }

        public async Task UpdateCategory(Guid id, UpdateCategoryDto input)
        {
            if (await _categoryRepository.GetAll().AnyAsync(c => c.Name == input.Name.ToLower().Trim() && c.Id != id))
                throw new Exception("Category name already exists.");

            var data = await _categoryRepository.GetByIdAsync(id);

            if (data == null)
                throw new Exception("Category was not found.");

            data.Name = input.Name.ToLower().Trim();
            data.Description = input.Description;
            data.Sort = input.Sort;

            _categoryRepository.Update(data);
            await _categoryRepository.SaveChangesAsync();
        }

    }
}
