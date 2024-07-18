using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITransactionServices
    {
        Task<int> CreateForBuySlot(TransactionCreateInfo info);
        Task DeleteSlot(int transaction_id);
        Task DeleteTran(int transaction_id);
        bool ExistTran(int tran_id);
        Task<TransactionDetail> GetDetail(int transaction_id);
        //Task<List<TransactionInfo>> GetOfUser(int user_id);
        Task<Transaction> GetTransaction(int transaction_id);
        //bool IsFromTwoPost(List<int>? idSlot);
        Task<int> UpdateStatus(TransactionCreateInfo info);
        //Task<int> CreateWithdrawRequest(CreateWithdrawRequest createWithdrawRequest);
    }
}
