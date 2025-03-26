using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public static class ToppingMappngs
    {
        public static ToppingGetVModel EntityToVModel(Topping size)
        {
            return new ToppingGetVModel
            {
                ToppingID = size.ToppingID,
                Name = size.Name,
                Price = size.Price,

            };
        }
        public static Topping VModelToEntity(ToppingGetVModel sizeVModel)
        {
            return new Topping
            {
                ToppingID = sizeVModel.ToppingID,
                Name = sizeVModel.Name,
                Price = sizeVModel.Price,
            };
        }
        public static Topping CreateVModelToEntity(ToppingCreateVModel vModel)
        {
            return new Topping
            {
                Name = vModel.Name,
                Price = vModel.Price,
            };
        }
    }
}
