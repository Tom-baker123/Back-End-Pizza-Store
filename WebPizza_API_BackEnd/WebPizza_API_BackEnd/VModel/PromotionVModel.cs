using OA.Domain.Common.Constants;

namespace WebPizza_API_BackEnd.VModel
{
    public class PromotionCreateVModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Active";
    }
    public class PromotionUpdateVModel : PromotionCreateVModel
    {
        public int PromotionID { get; set; }

    }
    public class PromotionGetVModel : PromotionUpdateVModel
    {
        public DateTime CreatedAt { get; set; }
    }
    public class PromotionFilterParams
    {
        public string? Name { get; set; }
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;

    }
}
