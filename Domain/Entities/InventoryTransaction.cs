using Domain.Enums;

namespace Domain.Entities
{
    public class InventoryTransaction
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateAt { get; set; }
        public TransactionType TransactionType { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
