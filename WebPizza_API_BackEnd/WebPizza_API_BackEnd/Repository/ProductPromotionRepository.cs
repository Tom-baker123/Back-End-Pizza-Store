using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;

namespace WebPizza_API_BackEnd.Repository
{
    public class ProductPromotionRepository : IProductPromotionRepo
    {
        public Task<ProductPromotion> AddAsync(ProductPromotion productPromotion)
        {
            return null;
        }

        public Task DeleteAsync(ProductPromotion productPromotion)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductPromotion>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductPromotion?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductPromotion productPromotion)
        {
            throw new NotImplementedException();
        }
    }
}
