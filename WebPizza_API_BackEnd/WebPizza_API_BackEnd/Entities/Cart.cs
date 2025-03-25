namespace WebPizza_API_BackEnd.Entities
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; }  // Khóa ngoại tới User
        public Product Product { get; set; }  // Khóa ngoại tới Product
    }
}
