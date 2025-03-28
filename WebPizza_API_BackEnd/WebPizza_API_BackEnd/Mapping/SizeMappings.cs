using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public static class SizeMappings
    {
        public static SizeGetVModel EntityToVModel(PizzaSize size)
        {
            return new SizeGetVModel
            {
                SizeID = size.SizeID,
                Name = size.Name,
                AdditionalPrice = size.AdditionalPrice,
            };
        }
        public static PizzaSize VModelToEntity(SizeGetVModel sizeVModel)
        {
            return new PizzaSize
            {
                SizeID = sizeVModel.SizeID,
                Name = sizeVModel.Name,
                AdditionalPrice = sizeVModel.AdditionalPrice,
            };
        }
        public static PizzaSize CreateVModelToEntity(SizeCreateVModel vModel)
        {
            return new PizzaSize
            {
                Name = vModel.Name,
                AdditionalPrice = vModel.AdditionalPrice,
            };
        }
    }
}
