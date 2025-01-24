using Microsoft.EntityFrameworkCore;
using Web_Pizza.Entities;

namespace Web_Pizza.Repositories
{
    public class UserRepository
    {
        private readonly PizzaStoreContext _pizzaStoreContext;
        public UserRepository(PizzaStoreContext pizzaStoreContext)
        {
            _pizzaStoreContext = pizzaStoreContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                _pizzaStoreContext.Users.Add(user);
                await _pizzaStoreContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu User: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Chi tiết lỗi: " + ex.InnerException.Message);
                }
                throw;
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _pizzaStoreContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByConfirmationTokenAsync(string token)
        {
            return await _pizzaStoreContext.Users
            .Where(u => u.ConfirmationToken == token)
            .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _pizzaStoreContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByPhoneAsync(string phone)
        {
            return await _pizzaStoreContext.Users.FirstOrDefaultAsync(u => u.Phone == phone);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Gắn trạng thái entity là Modified để EF nhận biết cần cập nhật
            _pizzaStoreContext.Users.Update(user);

            // Lưu thay đổi vào database
            await _pizzaStoreContext.SaveChangesAsync();

            return user; // Trả về user đã cập nhật
        }


    }
}
