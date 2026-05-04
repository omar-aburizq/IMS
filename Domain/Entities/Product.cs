namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrentStock { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    }
}
