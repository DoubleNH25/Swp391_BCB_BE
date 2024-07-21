using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.VisualBasic;
using Services.Interfaces;

namespace BadmintonMatching.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionRepository;
        private readonly IUserServices _userServices;

        public TransactionController(ITransactionServices transactionRepository, IUserServices userServices)
        {
            _transactionRepository = transactionRepository;
            _userServices = userServices;
        }

        [HttpPost]
        [Route("buy_slot")]
        public async Task<IActionResult> CreateTransactionBuyingSlot(TransactionCreateInfo info)
        {

            var tranId = await _transactionRepository.CreateForBuySlot(info);

            if (tranId == 0)
            {
                return Ok(new SuccessObject<object> { Message = "Tạo giao dịch thất bại !" });
            }
            else if (tranId == -1)
            {
                return Ok(new SuccessObject<object> { Message = "Số tiền trong ví không đủ để thanh toán !" });
            }
            else
            {
                return Ok(new SuccessObject<object> { Data = new { TranSactionId = tranId }, Message = Message.SuccessMsg });
            }
        }

    }
}
