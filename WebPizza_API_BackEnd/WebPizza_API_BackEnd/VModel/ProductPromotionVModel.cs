using OA.Domain.Common.Constants;
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
    }
    public class ProductPromotionGetVModel : ProductPromotionUpdateVModel
    {
        public ProductGetVModel Product { get; set; } = null!;
        public PromotionGetVModel Promotion { get; set; } = null!;
    }
    public class ProductPromotionFilterParams
    {
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;
    }
}
