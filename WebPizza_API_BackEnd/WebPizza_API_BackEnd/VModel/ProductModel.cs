namespace WebPizza_API_BackEnd.VModel
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedAt { get; set; }

        public CategoryModel Category { get; set; }
        public ICollection<CartModel> Carts { get; set; }
        public ICollection<OrderDetailModel> OrderDetails { get; set; }
        public ICollection<ProductSizeModel> ProductSizes { get; set; }
        public ICollection<ProductToppingModel> ProductToppings { get; set; }
        public ICollection<ProductPromotionModel> ProductPromotions { get; set; }
        public ICollection<ReviewModel> Reviews { get; set; }
    }
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CartModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }

    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
    }

    public class ProductSizeModel
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public SizeModel Size { get; set; }
    }

    public class SizeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductToppingModel
    {
        public int ProductId { get; set; }
        public int ToppingId { get; set; }
        public ToppingModel Topping { get; set; }
    }

    public class ToppingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductPromotionModel
    {
        public int ProductId { get; set; }
        public int PromotionId { get; set; }
        public PromotionModel Promotion { get; set; }
    }

    public class PromotionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ReviewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
    public class  ShortGetProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }

    }
}
