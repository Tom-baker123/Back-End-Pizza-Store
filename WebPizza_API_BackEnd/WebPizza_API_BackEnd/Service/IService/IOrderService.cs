using WebPizza_API_BackEnd.ViewModels.Order;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderVModel>> GetAllAsync();
        Task<OrderVModel> GetByIdAsync(int id);
        Task<OrderVModel> CreateAsync(CreateOrderVModel model);
        Task<OrderVModel> UpdateAsync(int id, UpdateOrderVModel model);
        Task DeleteAsync(int id);
    }
}
