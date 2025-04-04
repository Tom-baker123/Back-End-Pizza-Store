using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Mapping
{
    public static class CartMappings
    {
        public static CartCreateVModel ToCartCreateVModel(Cart cart)
        {
            return new CartCreateVModel
            {
                UserID = cart.UserID,
                ProductID = cart.ProductID,
                Quantity = cart.Quantity
            };
        }
        public static CartUpdateVModel ToCartUpdateVModel(Cart cart)
        {
            return new CartUpdateVModel
            {
                CartID = cart.CartID,
                UserID = cart.UserID,
                ProductID = cart.ProductID,
                Quantity = cart.Quantity
            };
        }
        public static CartGetVModel ToCartGetVModel(Cart cart)
        {
            var cartGetVModel = new CartGetVModel
            {
                CartID = cart.CartID,
                UserID = cart.UserID,
                ProductID = cart.ProductID,
                Quantity = cart.Quantity
            };
            if (cart.User != null)
            {
                cartGetVModel.User = new ShortUserVModel
                {
                    UserID = cart.User.UserID,
                    FullName = cart.User.FullName,
                    Email = cart.User.Email,
                    Phone = cart.User.Phone,
                    Address = cart.User.Address
                };
            }
            if (cart.Product != null)
            {
                cartGetVModel.Product = new ShortProductVModel
                {
                    ProductID = cart.Product.Id,
                    Name = cart.Product.Name,
                    Price = cart.Product.Price
                };
            }
            return cartGetVModel;
        }
        public static Cart ToCart(CartCreateVModel cartCreateVModel)
        {
            return new Cart
            {
                UserID = cartCreateVModel.UserID,
                ProductID = cartCreateVModel.ProductID,
                Quantity = cartCreateVModel.Quantity
            };
        }
        public static Cart ToCart(CartUpdateVModel cartUpdateVModel)
        {
            return new Cart
            {
                CartID = cartUpdateVModel.CartID,
                UserID = cartUpdateVModel.UserID,
                ProductID = cartUpdateVModel.ProductID,
                Quantity = cartUpdateVModel.Quantity
            };
        }
        public static Cart ToCart(CartGetVModel cartGetVModel)
        {
            return new Cart
            {
                CartID = cartGetVModel.CartID,
                UserID = cartGetVModel.UserID,
                ProductID = cartGetVModel.ProductID,
                Quantity = cartGetVModel.Quantity
            };
        }
    }
}
