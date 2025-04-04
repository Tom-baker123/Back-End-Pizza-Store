using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Domain.Common.Models;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Mapping;
using WebPizza_API_BackEnd.Service.IService;
using WebPizza_API_BackEnd.VModel;

namespace WebPizza_API_BackEnd.Service
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IUserService userService;
        public CartService(AppDbContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        public async Task<ResponseResult> Create(CartCreateVModel model)
        {
            try
            {
                var cartItem = CartMappings.ToCart(model);
                await _context.Carts.AddAsync(cartItem);
                await _context.SaveChangesAsync();
                return new SuccessResponseResult(cartItem, "Thêm sản phẩm vào giỏ hàng thành công");
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ActionResult<PaginationModel<CartGetVModel>>> GetAll()
        {
            var carts = await _context.Carts
                .Include(x => x.User)
                .Include(x => x.Product)
                .ToListAsync();
            var cartViewModels = carts.Select(x => CartMappings.ToCartGetVModel(x)).ToList();
            return new PaginationModel<CartGetVModel>
            {
                Records = cartViewModels,
                TotalRecords = cartViewModels.Count
            };
        }

        public async Task<ActionResult<CartGetVModel>?> GetbyId(int id)
        {
            var cart = await _context.Carts
                .Include(x => x.User)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.CartID == id);
            if (cart == null)
            {
                return null;
            }
            return CartMappings.ToCartGetVModel(cart);
        }

        public async Task<ResponseResult> Remove(int id)
        {
            var response = new ResponseResult();
            try
            {
                var cart = await _context.Carts.FindAsync(id);
                if (cart == null)
                {
                    return new ErrorResponseResult("Không tìm thấy sản phẩm trong giỏ hàng");
                }
                _context.Carts.Remove(cart);
                _context.SaveChanges();
                response = new SuccessResponseResult(cart, "Xóa sản phẩm khỏi giỏ hàng thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }

        public async Task<ResponseResult> Update(int id, CartUpdateVModel model)
        {
            var response = new ResponseResult();
            try
            {
                var cart = await _context.Carts.FindAsync(id);
                if (cart == null)
                {
                    return new ErrorResponseResult("Không tìm thấy sản phẩm trong giỏ hàng");
                }
                cart.Quantity = model.Quantity;
                _context.SaveChanges();
                response = new SuccessResponseResult(cart, "Cập nhật sản phẩm trong giỏ hàng thành công");
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponseResult(ex.Message);
            }
        }
    }
}
