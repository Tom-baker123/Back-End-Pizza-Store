namespace WebPizza_API_BackEnd.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Pending";
        public string? TransactionID { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public Order Order { get; set; } = null!;
    }
}
