using AutoMapper;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.ViewModels.Order;

namespace WebPizza_API_BackEnd.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderVModel>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return orders.Select(o => new OrderVModel
            {
                OrderID = o.OrderID,
                UserID = o.UserID,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                OrderDate = o.OrderDate,
                VoucherID = o.VoucherID
            });
        }

        public async Task<OrderVModel> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return null;
            return new OrderVModel
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDate = order.OrderDate,
                VoucherID = order.VoucherID
            };
        }

        public async Task<OrderVModel> AddOrderAsync(OrderCeateVModel orderVm)
        {
            var order = new Order
            {
                UserID = orderVm.UserID,
                TotalAmount = orderVm.TotalAmount,
                Status = orderVm.Status,
                OrderDate = orderVm.OrderDate,
                VoucherID = orderVm.VoucherID
            };
            var addedOrder = await _orderRepository.AddOrderAsync(order);
            return new OrderVModel
            {
                OrderID = addedOrder.OrderID,
                UserID = addedOrder.UserID,
                TotalAmount = addedOrder.TotalAmount,
                Status = addedOrder.Status,
                OrderDate = addedOrder.OrderDate,
                VoucherID = addedOrder.VoucherID
            };
        }

        public async Task<bool> UpdateOrderAsync(OrderVModel orderVm)
        {
            var order = new Order
            {
                UserID = orderVm.UserID,
                TotalAmount = orderVm.TotalAmount,
                Status = orderVm.Status,
                OrderDate = orderVm.OrderDate,
                VoucherID = orderVm.VoucherID
            };
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
