namespace WebPizza_API_BackEnd.Entities
{
    public class ProductSize
    {
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; } = null!;
        public Size Size { get; set; } = null!;
    }
}
