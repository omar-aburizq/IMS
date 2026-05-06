using Domain.Entities;
using Domain.Enums;

namespace Application.Services.InventoryTransactionService.DTOs
{
    public class CreateTransactionDto
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
