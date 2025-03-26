using OA.Domain.Common.Constants;

namespace WebPizza_API_BackEnd.VModel
{
    public class ToppingCreateVModel
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class ToppingUpdateVModel : ToppingCreateVModel
    {
        public int ToppingID { get; set; }
    }
    public class ToppingGetVModel : ToppingUpdateVModel
    {
    }
    public class ToppingFilterParams
    {
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;
    }
}
