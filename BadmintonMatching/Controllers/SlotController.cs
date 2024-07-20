using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Implements;
using Repositories.Intefaces;
using Services.Interfaces;

namespace BadmintonMatching.Controllers
{
    [Route("api/slots")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotServices _slotServices;
        private readonly ITransactionServices _transactionRepository;
        private readonly SlotServices _walletServices;
        private readonly IChatServices _chatServices;
        private readonly INotificationServices _notificationServices;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IPostServices _postServices;

        public SlotController(ISlotServices slotServices,
            ITransactionServices transactionRepository,
            SlotServices walletServices,
            IChatServices chatServices,
            INotificationServices notificationServices,
            IRepositoryManager repositoryManager,
            IPostServices postServices)
        {
            _slotServices = slotServices;
            _transactionRepository = transactionRepository;
            _walletServices = walletServices;
            _chatServices = chatServices;
            _notificationServices = notificationServices;
            _repositoryManager = repositoryManager;
            _postServices = postServices;
        }

        //    [HttpPost]
        //    [Route("booking")]
        //    public async Task<IActionResult> CheckAvailableAndCreateSlot(CheckAvailableSlot info)
        //    {
        //        try
        //        {
        //            List<SlotReturnInfo> slotInfos = _slotServices.GetAvailable(info);


        //            var lsSlot = new List<int>();
        //            var isDeleteSlot = false;
        //            foreach (var item in slotInfos)
        //            {
        //                if (item.SlotIds != null)
        //                {
        //                    foreach (var slotId in item.SlotIds)
        //                    {
        //                        lsSlot.Add(slotId);
        //                    }
        //                }
        //                else
        //                {
        //                    isDeleteSlot = true;
        //                }
        //            }

        //            if (isDeleteSlot)
        //            {
        //                _slotServices.Delete(lsSlot);
        //                return Ok(new SuccessObject<SlotIncludeTransaction>
        //                {
        //                    Message = "Số đầu vào vị trí có sẵn không hợp lệ !"
        //                });
        //            }

        //            var createInfo = new TransactionCreateInfo
        //            {
        //                IdSlot = lsSlot,
        //                IdUser = info.UserId
        //            };
        //            var tran = await _transactionRepository.CreateForBuySlot(createInfo);

        //            if (tran == null)
        //            {
        //                _slotServices.Delete(lsSlot);
        //                return Ok(new SuccessObject<object> { Message = "Không tìm thấy vị trí !" });
        //            }

        //            var newBalance = _walletServices.UpdateBalance(-tran.MoneyTrans.Value, createInfo.IdUser.Value, true);
        //            if (newBalance == -1 || newBalance == -2)
        //            {
        //                await _transactionRepository.DeleteSlot(tran.Id);
        //                await _transactionRepository.DeleteTran(tran.Id);
        //                if (newBalance == -1)
        //                {
        //                    return Ok(new SuccessObject<object> { Message = "Số dư không đủ để thanh toán !" });
        //                }
        //                else if (newBalance == -2)
        //                {
        //                    return Ok(new SuccessObject<object> { Message = $"Ví của người dùng {createInfo.IdUser.Value} không tìm thấy !" });
        //                }
        //            }
        //            else
        //            {
        //                await _transactionRepository.UpdateStatus(tran.Id, Entities.Models.TransactionStatus.PaymentSuccess);
        //            }

        //            var chatRoom = await _chatServices.GetChatRoom(tran.Id);
        //            var trans = await _repositoryManager.Transaction.FindByCondition(x => x.Id == tran.Id, false).FirstOrDefaultAsync();
        //            var check =  _notificationServices.TransactionDetailsEmail(trans);
        //            var valid  =  await _postServices.isValidPost(info.PostId);
        //            return Ok(new SuccessObject<SlotIncludeTransaction>
        //            {
        //                Data = new SlotIncludeTransaction
        //                {
        //                    TransactionId = tran.Id,
        //                    ChatInfos = chatRoom
        //                },
        //                Message = Message.SuccessMsg
        //            });
        //        }
        //        catch (FieldAccessException)
        //        {
        //            return Ok(new SuccessObject<object> { Message = "Không thể đăng ký bài viết của bạn !" });
        //        }
        //    }
        //}
    }
}
