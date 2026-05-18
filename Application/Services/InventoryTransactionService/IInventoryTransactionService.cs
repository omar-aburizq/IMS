using Application.Services.InventoryTransactionService.DTOs;

namespace Application.Services.InventoryTransactionService
{
    public interface IInventoryTransactionService
    {
        Task CreateTransaction(CreateTransactionDto input);
        Task<List<GetAllTransactionsDto>> GetAllTransactions();
        Task<GetTransactionByIdDto> GetTransactionById(Guid id);
    }
}
