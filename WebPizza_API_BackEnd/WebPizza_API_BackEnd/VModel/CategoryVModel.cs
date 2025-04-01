using OA.Domain.Common.Constants;

namespace WebPizza_API_BackEnd.VModel
{
    public class CategoryCreateVModel
    {
        public required string CategoryName { get; set; }
        //public bool IsActive { get; set; } = true;
    }
    public class CategoryUpdateVModel : CategoryCreateVModel
    {
        public int CategoryId { get; set; }
    }
    public class CategoryGetVModel : CategoryUpdateVModel
    {
    }
    public class CategoryFilterParams
    {
        public string? Name { get; set; }
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;
    }
    public class CategoryUpdate
    {
        public required string CategoryName { get; set; }
    }
}
    