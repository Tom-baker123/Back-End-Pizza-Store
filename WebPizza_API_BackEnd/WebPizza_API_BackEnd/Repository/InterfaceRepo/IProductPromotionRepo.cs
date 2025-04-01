using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.Repository.InterfaceRepo
{
    public interface IProductPromotionRepo
    {
        Task<List<ProductPromotion>> GetAllAsync();
        Task<ProductPromotion?> GetByIdAsync(int id);
        Task<ProductPromotion> AddAsync(ProductPromotion productPromotion);
        Task UpdateAsync(ProductPromotion productPromotion);
        Task DeleteAsync(ProductPromotion productPromotion);
    }
}
