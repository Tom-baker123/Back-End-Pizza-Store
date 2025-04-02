using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public class OrderDetailMapping
    {
        public static OrderDetailGetVModel EntityToVModel(OrderDetail orderDetail)
        {
            return new OrderDetailGetVModel
            {
                OrderDetailID = orderDetail.OrderDetailID,
                OrderID = orderDetail.OrderID,
                ProductID = orderDetail.ProductID,
                Quantity = orderDetail.Quantity,
                BasePrice = orderDetail.BasePrice,
                Subtotal = orderDetail.Subtotal
            };
        }

        public static OrderDetail VModelToEntity(OrderDetailGetVModel orderDetailVModel)
        {
            return new OrderDetail
            {
                OrderDetailID = orderDetailVModel.OrderDetailID,
                OrderID = orderDetailVModel.OrderID,
                ProductID = orderDetailVModel.ProductID,
                Quantity = orderDetailVModel.Quantity,
                BasePrice = orderDetailVModel.BasePrice,
                Subtotal = orderDetailVModel.Subtotal
            };
        }

        public static OrderDetail CreateVModelToEntity(OrderDetailCreateVModel vModel)
        {
            return new OrderDetail
            {
                OrderID = vModel.OrderID,
                ProductID = vModel.ProductID,
                Quantity = vModel.Quantity,
                BasePrice = vModel.BasePrice,
                Subtotal = vModel.Subtotal
            };
        }
    }
}
