using BadmintonMatching.Payment;
using Domain.Helpers;
using Domain.Libs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Interfaces;

namespace Services.Implements;

public class VNPayService : IVNPayService
{
    private readonly IConfiguration _configuration;
    private readonly string _paramUrlCallBack = "?payment_method=VnPay&payment_code={0}&success=1&order_id={1}";
    private readonly IOptions<VnPayOption> _options;

    public VNPayService(IConfiguration configuration, IOptions<VnPayOption> options)
    {
        _configuration = configuration;
        _options = options;
    }

    public ResponseUriModel CreatePayment(PaymentInfoModel model, HttpContext context)
    {
        var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_options.Value.TimeZoneId);
        var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
        var tick = DateTime.Now.Ticks.ToString();

        var pay = new VnPayLibrary();

        var urlCallBack = _configuration["PaymentConfig:ReturnUrl"];

        pay.AddRequestData("vnp_Version", _options.Value.Version);
        pay.AddRequestData("vnp_Command", _options.Value.Command);
        pay.AddRequestData("vnp_TmnCode", _options.Value.TmnCode);
        pay.AddRequestData("vnp_Amount", ((int)model.TotalAmount * 100).ToString());
        pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
        pay.AddRequestData("vnp_CurrCode", _options.Value.CurrCode);
        pay.AddRequestData("vnp_IpAddr", IPAdressHelper.GetIpAddress(context));
        pay.AddRequestData("vnp_Locale", _options.Value.Locale);
        pay.AddRequestData("vnp_OrderInfo", $"{model.PaymentCode}");
        pay.AddRequestData("vnp_OrderType", "other");
        pay.AddRequestData("vnp_ReturnUrl", urlCallBack + string.Format(_paramUrlCallBack, model.PaymentCode, tick));
        pay.AddRequestData("vnp_TxnRef", tick);

        var paymentUrl =
            pay.CreateRequestUrl(_options.Value.BaseUrl, _options.Value.HashSecret);

        return new ResponseUriModel()
        {
            Name = "PaymentUrl",
            Uri = paymentUrl
        };
    }

    public PaymentResponseModel PaymentExecute(IQueryCollection collection)
    {
        var response = PaymentHelper.GetParamPaymentCallBack(collection, _options.Value.HashSecret);

        return response.Adapt<PaymentResponseModel>();
    }
}