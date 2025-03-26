using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public static class SizeMappings
    {
        public static SizeGetVModel EntityToVModel(Size size)
        {
            return new SizeGetVModel
            {
                SizeID = size.SizeID,
                Name = size.Name,
                AdditionalPrice = size.AdditionalPrice,
            };
        }
        public static Size VModelToEntity(SizeGetVModel sizeVModel)
        {
            return new Size
            {
                SizeID = sizeVModel.SizeID,
                Name = sizeVModel.Name,
                AdditionalPrice = sizeVModel.AdditionalPrice,
            };
        }
        public static Size CreateVModelToEntity(SizeCreateVModel vModel)
        {
            return new Size
            {
                Name = vModel.Name,
                AdditionalPrice = vModel.AdditionalPrice,
            };
        }
    }
}
