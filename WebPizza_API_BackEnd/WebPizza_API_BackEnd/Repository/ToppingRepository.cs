using Microsoft.EntityFrameworkCore;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;

namespace WebPizza_API_BackEnd.Repository
{
    public class ToppingRepository : ITopingRepo
    {
        private readonly AppDbContext _context;

        public ToppingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Topping> AddAsync(Topping topping)
        {
            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();
            return topping;
        }

        public Task DeleteAsync(Topping topping)
        {
            _context.Toppings.Remove(topping);
            return _context.SaveChangesAsync();
        }

        public async Task<List<Topping>> GetAllAsync()
        {
            return await _context.Toppings
                .OrderByDescending(c => c.ToppingID)
                .ToListAsync();
        }

        public async Task<Topping?> GetByIdAsync(int id)
        {
            return await _context.Toppings
                .FirstOrDefaultAsync(s => s.ToppingID == id);

        }

        public async Task UpdateAsync(Topping topping)
        {
           _context.Toppings.Update(topping);
            await _context.SaveChangesAsync();
        }
    }
}
