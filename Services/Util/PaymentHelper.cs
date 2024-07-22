using Domain.Libs;
using Microsoft.AspNetCore.Http;

namespace Domain.Helpers;

public static class PaymentHelper
{
    public static PaymentResponse GetParamPaymentCallBack(IQueryCollection collection, string hashSecret = null)
    {
        var paymentMethod = "";

        foreach (var (key, value) in collection)
        {
            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payment_method"))
            {
                paymentMethod = value;
            }
        }

        switch (paymentMethod.ToLower())
        {
            case "vnpay":
                {
                    var vnPay = GetVnPayCallBack(collection, hashSecret);

                    return vnPay;
                }
            case "momo":
                {
                    var momo = GetMoMoCallBack(collection);

                    return momo;
                }
            case "paypal":
                {
                    var paypal = GetPayPalCallBack(collection);

                    return paypal;
                }
        }

        return new PaymentResponse();
    }

    private static PaymentResponse GetVnPayCallBack(IQueryCollection collection, string hashSecret)
    {
        var vnPay = new VnPayLibrary();

        foreach (var (key, value) in collection)
        {
            if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
            {
                vnPay.AddResponseData(key, value);
            }
        }

        var orderId = Convert.ToInt64(vnPay.GetResponseData("vnp_TxnRef"));
        var vnPayTranId = Convert.ToInt64(vnPay.GetResponseData("vnp_TransactionNo"));
        var vnpResponseCode = vnPay.GetResponseData("vnp_ResponseCode");
        var vnpSecureHash =
            collection.FirstOrDefault(k => k.Key == "vnp_SecureHash").Value; //hash của dữ liệu trả về
        var orderInfo = vnPay.GetResponseData("vnp_OrderInfo");
        var vnpAmount = vnPay.GetResponseData("vnp_Amount");

        var checkSignature =
            vnPay.ValidateSignature(vnpSecureHash, hashSecret); //check Signature

        if (!checkSignature)
            return new PaymentResponse()
            {
                Success = false
            };

        var paymentCode = "";

        foreach (var (key, value) in collection)
        {
            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payment_code"))
            {
                paymentCode = value;
            }
        }

        return new PaymentResponse()
        {
            Success = vnpResponseCode.Equals("00"),
            PaymentMethod = "VNPAY",
            BillId = orderInfo,
            OrderId = orderId.ToString(),
            PaymentId = vnPayTranId.ToString(),
            TransactionId = vnPayTranId.ToString(),
            Token = vnpSecureHash,
            PaymentCode = paymentCode,
            TotalAmount = ((int)(double.Parse(vnpAmount) / 100)).ToString()
        };
    }

    private static PaymentResponse GetMoMoCallBack(IQueryCollection collection)
    {
        var amount = collection.First(s => s.Key == "amount").Value;
        var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
        var orderId = collection.First(s => s.Key == "orderId").Value;
        var message = collection.First(s => s.Key == "message").Value.ToString();

        return new PaymentResponse()
        {
            Success = message.ToLower().Equals("success"),
            TotalAmount = amount,
            OrderId = orderId,
            PaymentCode = orderInfo,
            PaymentMethod = "MOMO"
        };
    }

    private static PaymentResponse GetPayPalCallBack(IQueryCollection collections)
    {
        var response = new PaymentResponse();
        var paymentCode = "";

        foreach (var (key, value) in collections)
        {
            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payment_code"))
            {
                paymentCode = value;
            }

            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("transaction_id"))
            {
                response.TransactionId = value;
            }

            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("order_id"))
            {
                response.OrderId = value;
            }

            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payment_method"))
            {
                response.PaymentMethod = value;
            }

            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("success"))
            {
                response.Success = Convert.ToInt32(value) > 0;
            }

            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("paymentid"))
            {
                response.PaymentId = value;
            }

            if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payerid"))
            {
                response.PayerId = value;
            }
        }

        response.PaymentMethod = "PAYPAL";
        response.PaymentCode = paymentCode;

        return response;
    }
}

public class PaymentResponse
{
    public string OwnerId { get; set; }
    public string UserId { get; set; }
    public string BillId { get; set; }
    public string TransactionId { get; set; }
    public string OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public string PayerId { get; set; }
    public string PaymentId { get; set; }
    public bool Success { get; set; }
    public string Token { get; set; }
    public string Customer { get; set; }
    public string TotalAmount { get; set; }
    public string PaymentCode { get; set; }
}