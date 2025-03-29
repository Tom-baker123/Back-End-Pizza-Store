using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.Repository.InterfaceRepo
{
    public interface ITopingRepo
    {
        Task<List<Topping>> GetAllAsync();
        Task<Topping?> GetByIdAsync(int id);
        Task<Topping> AddAsync(Topping topping);
        Task UpdateAsync(Topping topping);
        Task DeleteAsync(Topping topping);
    }
}
