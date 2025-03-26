using OA.Domain.Common.Constants;

namespace WebPizza_API_BackEnd.VModel
{
    public class SizeCreateVModel
    {
        public required string Name { get; set; }
        public decimal AdditionalPrice { get; set; } = 0;
    }
    public class SizeUpdateVModel : SizeCreateVModel
    {
        public int SizeID { get; set; }
    }
    public class SizeGetVModel : SizeUpdateVModel
    {
    }
    public class SizeFilterParams
    {
        //public bool? IsActive { get; set; }
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;
    }
}
