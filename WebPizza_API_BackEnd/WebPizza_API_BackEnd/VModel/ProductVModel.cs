using OA.Domain.Common.Constants;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.ViewModels
{
    public class ProductCreateVModel
    {
        // public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string? ImageURL { get; set; }

        // Thêm thông tin danh mục
        //   public CategoryGetVModel Category { get; set; }
    }

    public class ProductUpdateVModel : ProductCreateVModel
    {
        public int Id { get; set; }
    }

    public class ProductGetVModel : ProductUpdateVModel
    {
        public string? CategoryName { get; set; }
    }

    public class ProductFilterParams
    {
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;
    }

    public static class ProductMapper
    {
        public static ProductGetVModel EntityToVModel(Product product)
        {
            return new ProductGetVModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURL = product.ImageURL,
                CategoryID = product.CategoryID,
                CategoryName = product.Category?.CategoryName
            };
        }
    }
}

