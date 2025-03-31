using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Repository;
using WebPizza_API_BackEnd.Repository.InterfaceRepo;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepo _promotionRepo;
        public PromotionService(IPromotionRepo promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }
        public async Task<ResponseResult> Create(PromotionCreateVModel model)
        {
            try
            {
                var promotion = PromotionMappings.CreateVModelToEntity(model);
                await _promotionRepo.AddAsync(promotion);
                return new SuccessResponseResult(promotion, "Tạo promotion thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<PaginationModel<PromotionGetVModel>>> GetAll(PromotionFilterParams parameters)
        {
                var promotions = await _promotionRepo.GetAllAsync(parameters);
                var promotionViewModels = promotions.Select(x => PromotionMappings.ToPromotionGetVModel(x)).ToList();
                return new PaginationModel<PromotionGetVModel>
                {
                    Records = promotionViewModels,
                    TotalRecords = promotionViewModels.Count
                };
        }

        public async Task<ActionResult<PromotionGetVModel>?> GetbyId(int id)
        {
            var promotion = await _promotionRepo.GetByIdAsync(id);
            if(promotion == null)
            {
                return null;
            }
            return PromotionMappings.ToPromotionGetVModel(promotion);
        }

        public async Task<ResponseResult> Remove(int id)
        {
            var response = new ResponseResult();
            try
            {
                var promotion = await _promotionRepo.GetByIdAsync(id);
                if (promotion == null)
                {
                    return new ErrorResponseResult("Không tìm thấy promotion");
                }
                await _promotionRepo.DeleteAsync(promotion);
                response = new SuccessResponseResult(promotion, "Xóa promotion thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Update(int id,PromotionUpdateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var promotion = await _promotionRepo.GetByIdAsync(id);
                if (promotion == null)
                {
                    return new ErrorResponseResult("Không tìm thấy promotion");
                }
                promotion.Name = model.Name;
                promotion.Description = model.Description;
                promotion.StartDate = model.StartDate;
                promotion.EndDate = model.EndDate;
                promotion.Discount = model.Discount;
                promotion.Status = model.Status;
                await _promotionRepo.UpdateAsync(promotion);
                response = new SuccessResponseResult(promotion, "Cập nhật promotion thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
        
    }
}
