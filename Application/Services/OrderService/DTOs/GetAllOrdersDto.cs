namespace Application.Services.OrderService.DTOs
{
    public class GetAllOrdersDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreateAt { get; set; }
        public string UserName { get; set; }
    }
}
