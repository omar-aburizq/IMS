using Application.Repositories;
using Application.Services.OrderService.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, IGenericRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrder(CreateOrderDto input)
        {

            var lastOrder = _orderRepository.GetAll().OrderByDescending(x => x.CreateAt).FirstOrDefault();

            int nextNumber = 1;

            if (lastOrder != null && int.TryParse(lastOrder.Number, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                Number = nextNumber.ToString(),
                CreateAt = DateTime.UtcNow,
                UserId = input.UserId,
            };
            await _orderRepository.InsertAsync(order);
            await _orderRepository.SaveChangesAsync();


            var orderDetail = new List<OrderDetail>();

            decimal totalAmount = 0;

            foreach (var item in input.CreateOrderDetail)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                var unitPrice = product.SalePrice;
                totalAmount += unitPrice * item.Quantity;

                orderDetail.Add(new OrderDetail()
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                });
            }

            order.TotalAmount = totalAmount;

            await _orderDetailRepository.InsertRangeAsync(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
        }


        public async Task<List<GetAllOrdersDto>> GetAllOrders()
        {
            var data = _orderRepository.GetAll().Include(x => x.User);

            var result = await data.Select(c => new GetAllOrdersDto
            {
                Id = c.Id,
                Number = c.Number,
                TotalAmount = c.TotalAmount,
                CreateAt = c.CreateAt,
                UserName = c.User.Name,
            }).ToListAsync();

            return result;
        }

        public async Task<GetOrderByIdDto> GetOrderById(Guid id)
        {
            var data = await _orderRepository.GetAll().Include(x => x.User).Include(x => x.OrderDetails).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Order not found.");

            var result = new GetOrderByIdDto
            {
                Id = data.Id,
                Number = data.Number,
                CreateAt = data.CreateAt,
                TotalAmount = data.TotalAmount,
                UserName = data.User.Name,
                GetOrderDetailByIdDto = data.OrderDetails.Select(x => new GetOrderDetailByIdDto
                {
                    Id= x.Id,
                    ProductName=x.Product.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
            return result;
        }

    }
}
