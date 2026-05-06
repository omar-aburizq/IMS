using Domain.Entities;

namespace Application.Services.OrderService.DTOs
{
    public class GetOrderByIdDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreateAt { get; set; }
        public string? UserName { get; set; }
        public List<GetOrderDetailByIdDto> GetOrderDetailByIdDto { get; set; }
    }

    public class GetOrderDetailByIdDto
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
    }

}
