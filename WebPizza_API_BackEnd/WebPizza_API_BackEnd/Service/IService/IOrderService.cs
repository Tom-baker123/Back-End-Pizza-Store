using WebPizza_API_BackEnd.ViewModels.Order;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderVModel>> GetOrdersAsync();
        Task<OrderVModel> GetOrderByIdAsync(int id);
        Task<OrderVModel> AddOrderAsync(OrderVModel orderVm);
        Task<bool> UpdateOrderAsync(OrderVModel orderVm);
        Task<bool> DeleteOrderAsync(int id);
    }
}
