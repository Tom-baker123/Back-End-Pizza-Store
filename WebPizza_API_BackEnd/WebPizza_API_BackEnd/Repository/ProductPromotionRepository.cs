using Microsoft.EntityFrameworkCore;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;

namespace WebPizza_API_BackEnd.Repository
{
    public class ProductPromotionRepository : IProductPromotionRepo
    {
        private readonly AppDbContext _context;
        public ProductPromotionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ProductPromotion> AddAsync(ProductPromotion productPromotion)
        {
            _context.ProductPromotions.Add(productPromotion);
            await _context.SaveChangesAsync();
            return productPromotion;
        }

        public async Task DeleteAsync(ProductPromotion productPromotion)
        {
            _context.ProductPromotions.Remove(productPromotion);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductPromotion>> GetAllAsync()
        {
            return await _context.ProductPromotions
                .Include(x=>x.Product)
                .Include(x=>x.Promotion)
                .OrderByDescending(x => x.PromotionID)
                .ToListAsync();
        }

        public async Task<ProductPromotion?> GetByIdAsync(int productId, int promotionId)
        {
            return await _context.ProductPromotions
                .Include(x => x.Product)
                .Include(x => x.Promotion)
                .FirstOrDefaultAsync(x => x.ProductID == productId && x.PromotionID == promotionId);
        }

        public async Task UpdateAsync(ProductPromotion productPromotion)
        {
            _context.ProductPromotions.Update(productPromotion);
            await _context.SaveChangesAsync();
        }
    }
}
