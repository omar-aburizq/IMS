namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreateAt { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
