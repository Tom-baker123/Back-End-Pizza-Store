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
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderVModel>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderVModel>>(orders);
        }

        public async Task<OrderVModel> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }
            return _mapper.Map<OrderVModel>(order);
        }

        public async Task<OrderVModel> CreateAsync(CreateOrderVModel model)
        {
            var order = _mapper.Map<Order>(model);

            // Create related entities
            order.OrderDetails = _mapper.Map<ICollection<OrderDetail>>(model.OrderDetails);

            if (model.Payment != null)
            {
                order.Payment = _mapper.Map<Payment>(model.Payment);
                order.Payment.PaymentDate = DateTime.Now;
            }

            var createdOrder = await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderVModel>(createdOrder);
        }

        public async Task<OrderVModel> UpdateAsync(int id, UpdateOrderVModel model)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            _mapper.Map(model, order);
            await _orderRepository.UpdateAsync(order);

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            await _orderRepository.DeleteAsync(id);
        }
    }
}
