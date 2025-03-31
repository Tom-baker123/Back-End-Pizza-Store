using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Repository.InterfaceRepo
{
    public interface IPromotionRepo
    {
        Task<List<Promotion>> GetAllAsync(PromotionFilterParams parameters);
        Task<Promotion?> GetByIdAsync(int id);
        Task<Promotion> AddAsync(Promotion promotion);
        Task UpdateAsync(Promotion promotion);
        Task DeleteAsync(Promotion promotion);
    }
}
