using Application.Services.CategoryService;
using Application.Services.CategoryService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto input)
        {
            await _categoryService.CreateCategory(input);
            return Ok();
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return Ok(category);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto input)
        {
            await _categoryService.UpdateCategory(id, input);
            return Ok();
        }
    }
}
