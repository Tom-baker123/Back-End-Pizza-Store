using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;
using static WebPizza_API_BackEnd.Mapping.ProductMappings;

namespace WebPizza_API_BackEnd.Mapping
{
    public static class ProductPromotionMappings
    {
        public static ProductPromotionGetVModel EntityToVModel(ProductPromotion entity)
        {
            var productPromotion= new ProductPromotionGetVModel
            {
                ProductID = entity.ProductID,
                PromotionID = entity.PromotionID,            
            };
            var product=entity.Product;
            if (product != null)
            {
                productPromotion.Product = new VModel.ProductGetVModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageURL = product.ImageURL,
                    Price = product.Price,
                    CategoryID = product.CategoryID,
                    
                };
            }
            var promotion = entity.Promotion;
            if (promotion != null)
            {
                productPromotion.Promotion = new PromotionGetVModel
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
            return productPromotion;
        }
    }
}
