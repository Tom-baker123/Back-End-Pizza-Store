using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IPromotionService
    {
        Task<ActionResult<PaginationModel<PromotionGetVModel>>> GetAll(PromotionFilterParams parameters);
        Task<ActionResult<PromotionGetVModel>?> GetbyId(int id);
        Task<ResponseResult> Create(PromotionCreateVModel model);
        Task<ResponseResult> Update(int id,PromotionUpdateVModel model);
        Task<ResponseResult> Remove(int id);
    }
}
