using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.VModel
{
    public class CartCreateVModel
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
    public class CartUpdateVModel : CartCreateVModel
    {
        public int CartID { get; set; }
    }
    public class CartGetVModel : CartUpdateVModel
    {
        public ShortUserVModel User { get; set; }
        public ShortProductVModel Product { get; set; }
    }
    public class ShortUserVModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
    public class ShortProductVModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
    public class CartResponseVModel
    {
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public ICollection<ShortGetProduct> shortGetProducts { get; set; } = new List<ShortGetProduct>();
    }

}
