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
        [HttpPut]
        [Route("status_info")]
        [ProducesResponseType(typeof(SuccessObject<List<Reports>>), 200)]
        public async Task<IActionResult> ChangeStatus(TransactionCreateInfo info)
        {

            var res = await _transactionRepository.UpdateStatus(info);
            if (res == 1)
            {
                return Ok(new SuccessObject<object> { Message = "Cập nhật thành công", Data = true });

            }
            else
            {
                return Ok(new SuccessObject<object> { Message = "Giao dịch khôn tồn tại !" });
            }
        }
        [HttpGet]
        [Route("{transaction_id}/detail")]
        [ProducesResponseType(typeof(SuccessObject<TransactionDetail>), 200)]
        public async Task<IActionResult> GetTransactionDetail(int transaction_id)
        {
            var data = await _transactionRepository.GetDetail(transaction_id);
            return Ok(new SuccessObject<TransactionDetail> { Data = data, Message = Message.SuccessMsg });
        }
        [HttpDelete]
        [Route("{transaction_id}/discard")]
        public async Task<IActionResult> DiscardTransaction(int transaction_id)
        {
            var transaction = await _transactionRepository.GetTransaction(transaction_id);
            if (transaction.Id > 0)
            {
                if (transaction.Status == (int)TransactionStatus.Booked)
                {
                    await _transactionRepository.DeleteSlot(transaction_id);
                    await _transactionRepository.DeleteTran(transaction_id);
                    return Ok(new SuccessObject<object> { Message = Message.SuccessMsg, Data = true });
                }
                else
                {
                    return Ok(new SuccessObject<object> { Message = "Giao dịch đã hoàn tất không được phép xóa !" });
                }
            }
            else
            {
                return Ok(new SuccessObject<object> { Message = "Giao dịch khôn tồn tại ! id" });
            }
        }
    }
}
