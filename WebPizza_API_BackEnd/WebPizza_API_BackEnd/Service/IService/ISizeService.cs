using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface ISizeService
    {
        Task<ActionResult<PaginationModel<SizeGetVModel>>> GetAll();
        Task<ActionResult<SizeGetVModel>?> GetbyId(int id);
        Task<ActionResult<ResponseResult>> Create(SizeCreateVModel model);
        Task<ActionResult<ResponseResult>> Update(SizeUpdateVModel model);
        Task<ActionResult<ResponseResult>> Delete(int id);
    }
}