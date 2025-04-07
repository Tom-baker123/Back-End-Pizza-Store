using WebPizza_API_BackEnd.Common.Models;
using WebPizza_API_BackEnd.Entities;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IVnPayService
    {
        //string CreatePaymentUrl(PaymentInfomationModel model, HttpContext context);
        string CreatePaymentUrl(Order model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
