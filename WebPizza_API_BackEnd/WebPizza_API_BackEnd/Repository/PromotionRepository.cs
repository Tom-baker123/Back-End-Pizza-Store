using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Repository
{
    public class PromotionRepository : IPromotionRepo
    {
        private readonly AppDbContext _context;
        public PromotionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Promotion> AddAsync(Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return promotion;

        }

        public async Task DeleteAsync(Promotion promotion)
        {
            _context.Promotions.Remove(promotion);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Promotion>> GetAllAsync(PromotionFilterParams parameters)
        {
            return await _context.Promotions
                .Where(BuildQueryable(parameters))
                .OrderByDescending(c => c.PromotionID)
                .ToListAsync();
        }

        public async Task<Promotion?> GetByIdAsync(int id)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(s => s.PromotionID == id);
        }

        public async Task UpdateAsync(Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            await _context.SaveChangesAsync();
            
        }

        private Expression<Func<Promotion, bool>> BuildQueryable(PromotionFilterParams fParams)
        {
            return x =>
                (fParams.Name == null || (x.Name != null && x.Name.Contains(fParams.Name)));
        }
    }
}
