namespace WebPizza_API_BackEnd.Entities
{
    public class ProductPromotion
    {
        public int ProductID { get; set; }
        public int PromotionID { get; set; }

        public Product Product { get; set; }
        public Promotion Promotion { get; set; }
    }
}
