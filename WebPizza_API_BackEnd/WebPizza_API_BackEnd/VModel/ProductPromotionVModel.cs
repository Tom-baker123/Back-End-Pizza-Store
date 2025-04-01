using WebPizza_API_BackEnd.Entities;
//using WebPizza_API_BackEnd.ViewModels;

namespace WebPizza_API_BackEnd.VModel
{
    public class ProductPromotionCreateVModel
    {
        public int ProductID { get; set; }
        public int PromotionID { get; set; }
    }
    public class ProductPromotionUpdateVModel : ProductPromotionCreateVModel
    {
        public int ProductPromotionID { get; set; }
    }
    public class ProductPromotionGetVModel : ProductPromotionUpdateVModel
    {
        public ProductGetVModel Product { get; set; } = null!;
        public PromotionGetVModel Promotion { get; set; } = null!;
    }
}
