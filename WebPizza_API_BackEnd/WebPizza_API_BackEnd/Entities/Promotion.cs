namespace WebPizza_API_BackEnd.Entities
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<ProductPromotion> ProductPromotions { get; set; }
    }
}
