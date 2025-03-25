using CloudinaryDotNet.Actions;

namespace WebPizza_API_BackEnd.Entities
{
    public class Voucher
    {
        public int VoucherID { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal MinOrderValue { get; set; } = 0;
        public string Status { get; set; } = "Active";

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
