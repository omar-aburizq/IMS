using Application.Services.InventoryTransactionService;
using Application.Services.InventoryTransactionService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IInventoryTransactionService _inventoryTransactionService;
        public InventoryTransactionController(IInventoryTransactionService inventoryTransactionService)
        {
            _inventoryTransactionService = inventoryTransactionService;
        }

        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto input)
        {
            await _inventoryTransactionService.CreateTransaction(input);
            return Ok();
        }

        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var result = await _inventoryTransactionService.GetAllTransactions();
            return Ok(result);
        }

        [HttpGet("GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var result = await _inventoryTransactionService.GetTransactionById(id);
            return Ok(result);
        }
    }
}
