namespace WebPizza_API_BackEnd.Entities
{
    public class Topping
    {
        public int ToppingID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Danh sách quan hệ nhiều-nhiều với Product
        public ICollection<ProductTopping> ProductToppings { get; set; }

        // 🛠 **Cần thêm thuộc tính này để tránh lỗi**
        public ICollection<OrderTopping> OrderToppings { get; set; }
    }
}
