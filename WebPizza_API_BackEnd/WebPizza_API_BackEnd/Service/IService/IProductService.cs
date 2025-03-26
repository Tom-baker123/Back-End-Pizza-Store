using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.ViewModels;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IProductService
    {
        Task<ActionResult<PaginationModel<ProductGetVModel>>> GetAll();
        Task<ActionResult<ProductGetVModel>?> GetById(int id);
        Task<ResponseResult> Create(ProductCreateVModel model);
        Task<ResponseResult> Update(int id, ProductUpdateVModel model);
        Task<ResponseResult> Remove(int id);
    }
}
