using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;

namespace WebPizza_API_BackEnd.Repository
{
    public class SizeRepository : ISizeRepo
    {
        private readonly AppDbContext _context;

        public SizeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PizzaSize>> GetAllAsync()
        {
            return await _context.Sizes
                .OrderByDescending(c => c.SizeID)
                .ToListAsync();
        }

        public async Task<PizzaSize?> GetByIdAsync(int id)
        {
            return await _context.Sizes
                .FirstOrDefaultAsync(s => s.SizeID == id);
        }

        public async Task<PizzaSize> AddAsync(PizzaSize size)
        {
            _context.Sizes.Add(size);
            await _context.SaveChangesAsync();
            return size;
        }

        public async Task UpdateAsync(PizzaSize size)
        {
            _context.Sizes.Update(size);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PizzaSize size)
        {
            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();
        }
    }
}
