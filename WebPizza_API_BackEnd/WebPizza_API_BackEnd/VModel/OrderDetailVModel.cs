using OA.Domain.Common.Constants;
using System.Text.Json.Serialization;

namespace WebPizza_API_BackEnd.VModel
{
    public class OrderDetailCreateVModel
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal BasePrice { get; set; } = 0;
        public decimal Subtotal { get; set; }
    }

    public class OrderDetailUpdateVModel : OrderDetailCreateVModel
    {
        public int OrderDetailID { get; set; }
    }

    public class OrderDetailGetVModel : OrderDetailUpdateVModel
    {
    }

    public class OrderDetailFilterParams
    {
        public int PageSize { get; set; } = Numbers.Pagination.DefaultPageSize;
        public int PageNumber { get; set; } = Numbers.Pagination.DefaultPageNumber;
    }

}
