using Microsoft.EntityFrameworkCore;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Repository
{
    public class ProductRepository : IProductRepo
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }
        public async Task<Product> GetByIdAsyncW(int id)
        {
            return await _context.Products
                // Include thông tin Category (quan hệ 1-1)
                .Include(p => p.Category)

                // Include danh sách Cart (quan hệ 1-N)
                .Include(p => p.Carts)

                // Include danh sách OrderDetails (quan hệ 1-N)
                .Include(p => p.OrderDetails)

                // Include danh sách ProductSizes và thông tin Size liên quan (quan hệ N-N)
                .Include(p => p.ProductSizes)
                    .ThenInclude(ps => ps.Size)

                // Include danh sách ProductToppings và thông tin Topping liên quan (quan hệ N-N)
                .Include(p => p.ProductToppings)
                    .ThenInclude(pt => pt.Topping)

                // Include danh sách ProductPromotions và thông tin Promotion liên quan (quan hệ N-N)
                .Include(p => p.ProductPromotions)
                    .ThenInclude(pp => pp.Promotion)

                // Include danh sách Reviews (quan hệ 1-N)
                .Include(p => p.Reviews)

                // Tìm sản phẩm theo ID
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
