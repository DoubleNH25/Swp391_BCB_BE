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

      
    }
}
