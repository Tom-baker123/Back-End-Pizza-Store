namespace WebPizza_API_BackEnd.Entities
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; } = 0;
        public decimal Subtotal { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
        public ICollection<OrderTopping> OrderToppings { get; set; }
    }
}
