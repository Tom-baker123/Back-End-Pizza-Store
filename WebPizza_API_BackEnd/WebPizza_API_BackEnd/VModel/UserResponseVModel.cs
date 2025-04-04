using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.VModel
{
    public class UserResponseVModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CartResponseVModel> Carts { get; set; } = new List<CartResponseVModel>();

    }
}
