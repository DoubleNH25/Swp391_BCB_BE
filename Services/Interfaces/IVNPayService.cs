using BadmintonMatching.Payment;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IVNPayService
{
    ResponseUriModel CreatePayment(PaymentInfoModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collection);
}