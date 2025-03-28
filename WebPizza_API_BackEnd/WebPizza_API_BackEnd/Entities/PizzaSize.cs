namespace WebPizza_API_BackEnd.Entities
{
    public class PizzaSize
    {
        public int SizeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal AdditionalPrice { get; set; } = 0;

        public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
    }
}
