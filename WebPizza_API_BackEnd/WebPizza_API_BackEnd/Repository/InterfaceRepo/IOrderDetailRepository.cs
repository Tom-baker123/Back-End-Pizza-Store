using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.Repository.InterfaceRepo
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetAllAsync();
        Task<OrderDetail?> GetByIdAsync(int id);
        Task<OrderDetail> AddAsync(OrderDetail orderdetail);
        Task UpdateAsync(OrderDetail orderdetail);
        Task DeleteAsync(OrderDetail orderdetail);
    }
}
