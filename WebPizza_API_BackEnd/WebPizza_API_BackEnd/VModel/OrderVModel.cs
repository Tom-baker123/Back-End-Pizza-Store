// ViewModels/Order/OrderVModel.cs
using System.ComponentModel.DataAnnotations;

namespace WebPizza_API_BackEnd.ViewModels.Order
{
    public class OrderVModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int? VoucherID { get; set; }

        public UserVModel User { get; set; }
        public VoucherVModel Voucher { get; set; }
        public ICollection<OrderDetailVModel> OrderDetails { get; set; }
        public PaymentVModel Payment { get; set; }
    }

    public class CreateOrderVModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";
        public int? VoucherID { get; set; }

        [Required]
        public List<OrderDetailItemVModel> OrderDetails { get; set; }
        public PaymentItemVModel Payment { get; set; }
    }

    public class UpdateOrderVModel
    {
        [Required]
        public string Status { get; set; }
        public int? VoucherID { get; set; }
    }

    public class UserVModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class VoucherVModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
    }

    public class OrderDetailVModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductVModel Product { get; set; }
    }

    public class OrderDetailItemVModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }

    public class PaymentVModel
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }

    public class PaymentItemVModel
    {
        [Required]
        public string Method { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        public string Status { get; set; } = "Pending";
    }

    public class ProductVModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}