using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class ProductPromotionService : IProductPromotionService
    {
        private readonly IProductPromotionRepo _productPromotionRepo;
        public ProductPromotionService(IProductPromotionRepo productPromotionRepo)
        {
            _productPromotionRepo = productPromotionRepo;
        }
        public async Task<ResponseResult> Create(ProductPromotionCreateVModel model)
        {
            var saved = ProductPromotionMappings.CreateVModelToEntity(model);
            await _productPromotionRepo.AddAsync(saved);
            return new SuccessResponseResult(saved, "Tạo promotion thành công");
        }

        public async Task<ActionResult<PaginationModel<ProductPromotionGetVModel>>> GetAll(ProductPromotionFilterParams parameters)
        {
            var list = await _productPromotionRepo.GetAllAsync();

            var ds = list.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                 .Take(parameters.PageSize).Select(x => ProductPromotionMappings.EntityToVModel(x)).ToList();

            return new PaginationModel<ProductPromotionGetVModel>
            {
                Records = ds,
                TotalRecords = ds.Count
            };
        }

        public async Task<ActionResult<ProductPromotionGetVModel>?> GetbyId(int productId, int promotionId)
        {
            var productPromotion = await _productPromotionRepo.GetByIdAsync(productId, promotionId);
            if (productPromotion == null)
            {
                return null;
            }
            var result = ProductPromotionMappings.EntityToVModel(productPromotion);
            return result;
        }

        public async Task<ResponseResult> Remove(int productId, int promotionId)
        {
            var productPromotion =await _productPromotionRepo.GetByIdAsync(productId, promotionId);
            if (productPromotion == null)
            {
                return new ErrorResponseResult("Không tìm thấy promotion");
            }
            await _productPromotionRepo.DeleteAsync(productPromotion);
            return new SuccessResponseResult(productPromotion, "Xóa promotion thành công");
        }

        public async Task<ResponseResult> Update(int productId, int promotionId, ProductPromotionUpdateVModel model)
        {
            var productPromotion = await _productPromotionRepo.GetByIdAsync(productId,promotionId);
            if (productPromotion == null)
            {
                return new ErrorResponseResult("Không tìm thấy promotion");
            }
            productPromotion.ProductID = model.ProductID;
            productPromotion.PromotionID = model.PromotionID;
            await _productPromotionRepo.UpdateAsync(productPromotion);
            return new SuccessResponseResult(productPromotion, "Cập nhật promotion thành công");
        }
    }
}
