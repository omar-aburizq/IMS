using Application.Services.InventoryTransactionService.DTOs;

namespace Application.Services.InventoryTransactionService
{
    public interface IInventoryTransactionService
    {
        public Task CreateTransaction(CreateTransactionDto input);
        public Task<List<GetAllTransactionsDto>> GetAllTransactions();
        public Task<GetTransactionByIdDto> GetTransactionById(Guid id);
    }
}
