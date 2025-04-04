using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface ICartService
    {
        Task<ActionResult<PaginationModel<CartGetVModel>>> GetAll();
        Task<ActionResult<CartGetVModel>?> GetbyId(int id);
        Task<ResponseResult> Create(CartCreateVModel model);
        Task<ResponseResult> Update(int id, CartUpdateVModel model);
        Task<ResponseResult> Remove(int id);
    }
}
