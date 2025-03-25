using CloudinaryDotNet.Actions;

namespace WebPizza_API_BackEnd.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } = "Customer";
        public bool IsActive { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        // Quan hệ 1-N với Carts
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
    
