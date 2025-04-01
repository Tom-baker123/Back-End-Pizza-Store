using AutoMapper;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Cart, CartModel>();
            CreateMap<OrderDetail, OrderDetailModel>();
            CreateMap<ProductSize, ProductSizeModel>();
            //CreateMap<Size, SizeModel>();
            CreateMap<ProductTopping, ProductToppingModel>();
            CreateMap<Topping, ToppingModel>();
            CreateMap<ProductPromotion, ProductPromotionModel>();
            CreateMap<Promotion, PromotionModel>();
            CreateMap<Review, ReviewModel>();
        }
    }
}
