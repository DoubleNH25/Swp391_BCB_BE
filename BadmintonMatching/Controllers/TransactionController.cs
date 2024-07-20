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
    }
}
