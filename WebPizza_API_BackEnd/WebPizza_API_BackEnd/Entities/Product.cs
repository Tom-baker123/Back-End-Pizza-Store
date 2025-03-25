namespace WebPizza_API_BackEnd.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Category Category { get; set; }

        // Quan hệ 1-N với Cart
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        // Quan hệ 1-N với OrderDetails
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // Quan hệ nhiều-nhiều với Size
        public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

        // Quan hệ nhiều-nhiều với Topping
        public ICollection<ProductTopping> ProductToppings { get; set; } = new List<ProductTopping>();

        // Quan hệ nhiều-nhiều với Promotion
        public ICollection<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();

        // Quan hệ 1-N với Review
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
