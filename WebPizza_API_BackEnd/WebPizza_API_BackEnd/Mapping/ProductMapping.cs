using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.Mapping
{
    public class ProductMappings
    {
        public class ProductGetVModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImageURL { get; set; }
            public int CategoryID { get; set; }
            //  public string CategoryName { get; set; }
        }

        public static class ProductMapper
        {
            public static ProductGetVModel EntityToVModel(Product product)
            {
                return new ProductGetVModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageURL = product.ImageURL,
                    CategoryID = product.CategoryID
                    //CategoryName = product.Category?.CategoryName
                };
            }
        }
    }
}
