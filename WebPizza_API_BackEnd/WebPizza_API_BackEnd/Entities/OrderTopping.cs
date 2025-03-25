namespace WebPizza_API_BackEnd.Entities
{
    public class OrderTopping
    {
        public int OrderDetailID { get; set; }
        public int ToppingID { get; set; }
        public decimal Price { get; set; }

        public OrderDetail OrderDetail { get; set; }
        public Topping Topping { get; set; }
    }
}
