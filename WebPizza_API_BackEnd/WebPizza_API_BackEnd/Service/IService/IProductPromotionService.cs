using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IProductPromotionService
    {
        Task<ActionResult<PaginationModel<ProductPromotionGetVModel>>> GetAll(ProductPromotionFilterParams parameters);
        Task<ActionResult<ProductPromotionGetVModel>?> GetbyId(int productId, int promotionId);
        Task<ResponseResult> Create(ProductPromotionCreateVModel model);
        Task<ResponseResult> Update(int productId, int promotionId, ProductPromotionUpdateVModel model);
        Task<ResponseResult> Remove(int productId, int promotionId);
    }
}
