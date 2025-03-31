using OA.Domain.Common.Constants;

namespace WebPizza_API_BackEnd.VModel
{
    public class PromotionCreateVModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Discount { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public string Status { get; set; } = "Active";
    }
    public class PromotionUpdateVModel : PromotionCreateVModel
    {
        public int PromotionID { get; set; }
    }
    public class PromotionGetVModel : PromotionUpdateVModel
    {
    }
    public class PromotionFilterParams
    {
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;
    }
}
