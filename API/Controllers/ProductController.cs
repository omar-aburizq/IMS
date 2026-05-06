using Application.Services.ProductService;
using Application.Services.ProductService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto input)
        {
            await _productService.CreateProduct(input);
            return Ok();
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var users = await _productService.GetAllProducts();
            return Ok(users);
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var user = await _productService.GetProductById(id);
            return Ok(user);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto input)
        {
            await _productService.UpdateProduct(id, input);
            return Ok();
        }

    }
}
