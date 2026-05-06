using Application.Services.OrderService.DTOs;

namespace Application.Services.OrderService
{
    public interface IOrderService
    {
        public Task CreateOrder(CreateOrderDto input);
        public Task<List<GetAllOrdersDto>> GetAllOrders();
        public Task<GetOrderByIdDto> GetOrderById(Guid id);
    }
}
