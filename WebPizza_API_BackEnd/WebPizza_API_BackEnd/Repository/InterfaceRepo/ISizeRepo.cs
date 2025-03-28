using System.Drawing;
using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.Repository.InterfaceRepo
{
    public interface ISizeRepo
    {
        Task<List<PizzaSize>> GetAllAsync();
        Task<PizzaSize?> GetByIdAsync(int id);
        Task<PizzaSize> AddAsync(PizzaSize size);
        Task UpdateAsync(PizzaSize size);
        Task DeleteAsync(PizzaSize size);
    }
}
