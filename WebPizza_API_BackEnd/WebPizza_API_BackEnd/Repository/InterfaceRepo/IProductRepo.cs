using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Mapping;
using static WebPizza_API_BackEnd.Mapping.ProductMappings;

namespace WebPizza_API_BackEnd.Repository.InterfaceRepo
{
    public interface IProductRepo
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);



        Task SaveChangesAsync();
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> GetByIdAsyncW(int id);
    }
}
