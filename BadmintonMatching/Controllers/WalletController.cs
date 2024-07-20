using BadmintonMatching.Payment;
using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Interfaces;

namespace BadmintonMatching.Controllers
{
    [Route("api/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly Services.Interfaces.SlotServices _walletServices;
        private readonly IVNPayService _vnPayService;
        private readonly IOptions<VnPayOption> _options;

        public WalletController(Services.Interfaces.SlotServices walletServices, IVNPayService vnPayService, IOptions<VnPayOption> options)
        {
            _walletServices = walletServices;
            _vnPayService = vnPayService;
            _options = options;
        }

        [HttpPut]
        [Route("{user_id}")]
        public IActionResult UpdateWallet(int user_id, UpdateWallet updateWallet)
        {
            var newBalance = _walletServices.UpdateBalance(updateWallet.Changes, user_id, false);
            if (newBalance == -1)
            {
                return Ok(new SuccessObject<object> { Message = "Số dư không đủ để tahnh toán !" });
            }
            else if (newBalance == -2)
            {
                return Ok(new SuccessObject<object> { Message = $"Ví của người dùng {user_id} không tìm thấy !" });
            }
            else
            {
                return Ok(new SuccessObject<object> { Data = new { NewBalance = newBalance }, Message = Message.SuccessMsg });
            }
        }

        [HttpPost]
        [Route("create-vnpay")]
        public async Task<IActionResult> CreateVnPay(UpdateWallet wallet)
        {
            var responseUriVnPay = _vnPayService.CreatePayment(new PaymentInfoModel()
            {
                TotalAmount = (double)wallet.Changes,
                PaymentCode = wallet.UserId + "." + Guid.NewGuid()
            }, HttpContext);

            if (string.IsNullOrEmpty(responseUriVnPay.Uri))
            {
                return new BadRequestObjectResult(new
                {
                    Message = "Không thể tạo url thanh toán vào lúc này !"
                });
            }

            return Ok(new SuccessObject<object>
            {
                Message = "Tạo url thành công!",
                Data = responseUriVnPay
            });
        }

        [HttpGet]
        [Route("vnpay-callback")]
        public async Task<IActionResult> VnPayCallback()
        {
            var vnPayResponse = _vnPayService.PaymentExecute(Request.Query);

            if (!vnPayResponse.Success)
            {
                return Redirect(_options.Value.FEUrlCallback + "?success=false");
            }

            var paymentCodeSplit = vnPayResponse.PaymentCode.Split(".");

            if (paymentCodeSplit.Length != 2)
            {
                return Redirect(_options.Value.FEUrlCallback + "?success=false");
            }

            var userId = int.Parse(paymentCodeSplit.First());
            var money = double.Parse(vnPayResponse.TotalAmount);

            // Thực hiện nạp tiền tại đây
            // TO-DO

            var transactionId = string.Empty; // Lấy transaction id và gán lại

            return Redirect(_options.Value.FEUrlCallback + $"?success=true&amount={money}");
        }

        [HttpGet]
        [Route("user/{user_id}/history")]
        public async Task<IActionResult> GetWalletHistory(int user_id)
        {
            List<HistoryWalletModel> history = _walletServices.GetHistory(user_id);

            if (history.Count() == 0)
            {
                return Ok(new SuccessObject<object> { Data = null, Message = "Lịch sử không tồn tại !" });
            }

            return Ok(new SuccessObject<List<HistoryWalletModel>> { Data = history, Message = "Không tìm thấy lịch sử !" });
        }

        [HttpGet]
        [Route("{user_id}/user_wallet")]
        public async Task<IActionResult> GetUserWaller(int user_id)
        {
            var userWallet = await _walletServices.getWallerByUserId(user_id);
            if (userWallet.Id != 0)
            {
                return Ok(new SuccessObject<object> { Data = new { balance = userWallet.Balance }, Message = Message.SuccessMsg });
            }
            {
                return Ok(new SuccessObject<object> { Data = null, Message = "Không tìm thấy ví có id người dùng là " + user_id.ToString() });
            }
        }
    }
}
