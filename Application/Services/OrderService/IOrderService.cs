using Application.Services.OrderService.DTOs;

namespace Application.Services.OrderService
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto input);
        Task<List<GetAllOrdersDto>> GetAllOrders();
        Task<GetOrderByIdDto> GetOrderById(Guid id);
    }
}
