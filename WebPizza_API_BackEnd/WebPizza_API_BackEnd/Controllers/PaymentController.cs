using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Service.IService;

namespace WebPizza_API_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        public PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }
        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInfomationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }
        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Ok(response);
        }

    }
}
