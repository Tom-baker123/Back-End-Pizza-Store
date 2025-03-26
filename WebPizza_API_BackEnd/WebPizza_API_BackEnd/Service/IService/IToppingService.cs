using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IToppingService
    {
        Task <ActionResult<PaginationModel<ToppingGetVModel>>> GetAll();
        Task<ActionResult<ToppingGetVModel>?> GetbyId(int id);
        Task <ActionResult<ResponseResult>> Create(ToppingCreateVModel model);
        Task<ActionResult<ResponseResult>> Update(ToppingUpdateVModel model);
        Task<ActionResult<ResponseResult>> Delete(int id);

    }
}
