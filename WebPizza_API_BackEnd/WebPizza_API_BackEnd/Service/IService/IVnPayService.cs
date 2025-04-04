using WebPizza_API_BackEnd.Common.Models;

namespace WebPizza_API_BackEnd.Service.IService
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInfomationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
