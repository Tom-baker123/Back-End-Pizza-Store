// ViewModels/Order/OrderVModel.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebPizza_API_BackEnd.ViewModels.Order
{
    public class OrderVModel
    {
       // [JsonIgnore] // Ẩn OrderID khi nhận request từ client
        public int? OrderID { get; set; }
        public int UserID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }  
        public DateTime OrderDate { get; set; }
        public int? VoucherID { get; set; }
    }
    public class OrderCeateVModel
    {
        [JsonIgnore] // Ẩn OrderID khi nhận request từ client
        public int? OrderID { get; set; }
        public int UserID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int? VoucherID { get; set; }
    }
}