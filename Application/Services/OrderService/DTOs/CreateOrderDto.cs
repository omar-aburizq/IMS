namespace Application.Services.OrderService.DTOs
{
    public class CreateOrderDto
    {
        public Guid? UserId { get; set; }
        public List<CreateOrderDetailDto> CreateOrderDetail { get; set; }
    }

    public class CreateOrderDetailDto
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }

}
