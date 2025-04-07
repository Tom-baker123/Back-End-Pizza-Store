using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Context;
using WebPizza_API_BackEnd.Entities;
using WebPizza_API_BackEnd.Service.IService;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly AppDbContext _context;
        public PaymentController(IVnPayService vnPayService,AppDbContext context)
        {
            _vnPayService = vnPayService;
            _context = context;
        }
        //[HttpPost]
        //public IActionResult CreatePaymentUrlVnpay(PaymentInfomationModel model)
        //{
        //    var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
        //    if(url == null)
        //    {
        //        return BadRequest("Lỗi tạo url thanh toán");
        //    }
        //    return Ok(url);
        //}
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrlVnpay(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound("Đơn hàng không tồn tại");
            }
            var url = _vnPayService.CreatePaymentUrl(order, HttpContext);
            if (url == null)
            {
                return BadRequest("Lỗi tạo url thanh toán");
            }
            return Ok(url);
        }
        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Ok(response);
        }

    }
}
