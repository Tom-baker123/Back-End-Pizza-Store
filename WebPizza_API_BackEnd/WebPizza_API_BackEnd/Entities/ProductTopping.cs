namespace WebPizza_API_BackEnd.Entities
{
    public class ProductTopping
    {
        public int ProductID { get; set; }
        public int ToppingID { get; set; }

        public Product Product { get; set; } = null!;
        public Topping Topping { get; set; } = null!;
    }
}
