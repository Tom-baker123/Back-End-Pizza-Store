using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IOrderDetailService
    {

        Task<ActionResult<PaginationModel<OrderDetailGetVModel>>> GetAll(OrderDetailFilterParams parameters);
        Task<ActionResult<OrderDetailGetVModel>?> GetById(int id);
        Task<ActionResult<ResponseResult>> Create(OrderDetailCreateVModel model);
        Task<ActionResult<ResponseResult>> Update(OrderDetailUpdateVModel model);
        Task<ActionResult<ResponseResult>> Delete(int id);

    }
}
