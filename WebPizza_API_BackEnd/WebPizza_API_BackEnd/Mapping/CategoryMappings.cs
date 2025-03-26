using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public static class CategoryMappings
    {
        public static CategoryGetVModel EntityToVModel(Category category)
        {
            return new CategoryGetVModel
            {
                CategoryId = category.CategoryID,
                CategoryName = category.CategoryName,
            };
        }
        public static Category VModelToEntity(CategoryGetVModel categoryVModel)
        {
            return new Category
            {
                CategoryID = categoryVModel.CategoryId,
                CategoryName = categoryVModel.CategoryName,
            };
        }

        public static Category CreateVModelToEntity(CategoryCreateVModel vModel)
        {
            return new Category
            {
                CategoryName = vModel.CategoryName,
            };
        }
        public static Category UpdateVModelToEntity(CategoryUpdateVModel vModel)
        {
            return new Category
            {
                CategoryID = vModel.CategoryId,
                CategoryName = vModel.CategoryName,
            };
        }
    }
}
