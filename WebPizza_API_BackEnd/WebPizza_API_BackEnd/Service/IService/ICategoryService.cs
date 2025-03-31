using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface ICategoryService
    {
        Task<ActionResult<PaginationModel<CategoryGetVModel>>> GetAll(CategoryFilterParams parameters);
        Task<ActionResult<CategoryGetVModel>?> GetbyId(int id);
        Task<ResponseResult> Create(CategoryCreateVModel model);
        Task<ResponseResult> Update(int id, CategoryUpdate model);
        Task<ResponseResult> Remove(int id);
    }
}
