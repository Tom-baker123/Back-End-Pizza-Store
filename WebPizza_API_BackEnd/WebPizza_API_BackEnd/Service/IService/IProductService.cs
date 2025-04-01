using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.ViewModels.Order;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IProductService
    {
        Task<ActionResult<PaginationModel<ProductGetVModel>>> GetAll(ProductFilterParams parameters);
        Task<ActionResult<ProductGetVModel>?> GetById(int id);
        Task<ResponseResult> Create(ProductCreateVModel model, string imageUrl);
        Task<ResponseResult> Update(int id, ProductUpdateVModel model);
        Task<ResponseResult> Remove(int id);
        Task<Product> GetProductByNameAsync(string name);
        Task<ProductModel> GetProductWithDetailsAsync(int id);
    }
}

