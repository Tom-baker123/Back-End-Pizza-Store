using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public static class PromotionMappings
    {
        public static PromotionGetVModel ToPromotionGetVModel(Promotion promotion)
        {
            return new PromotionGetVModel
            {
                PromotionID = promotion.PromotionID,
                Name = promotion.Name,
                Description = promotion.Description,
                Discount = promotion.Discount,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                Status = promotion.Status,
                CreatedAt = promotion.CreatedAt
            };
        }
        public static Promotion CreateVModelToEntity(PromotionCreateVModel model)
        {
            return new Promotion
            {
                Name = model.Name,
                Description = model.Description,
                Discount = model.Discount,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status
            };
        }
        public static Promotion UpdateVModelToEntity(PromotionUpdateVModel model)
        {
            return new Promotion
            {
                PromotionID = model.PromotionID,
                Name = model.Name,
                Description = model.Description,
                Discount = model.Discount,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status
            };
        }
    }
}
