using Entities.Models;
using Entities.ResponseObject;
using Microsoft.EntityFrameworkCore;
using Repositories.Intefaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
namespace Services.Implements
{
    public class WalletServices : Interfaces.SlotServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public WalletServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public List<HistoryWalletModel> GetHistory(int user_id)
        {
            var histories = _repositoryManager.HistoryWallet
                .FindByCondition(x => x.UserId == user_id, false)
                .Select(x => new HistoryWalletModel
                {
                    IdWallet = x.IdWallet,
                    Id = x.Id,
                    IdUser = x.UserId,
                    Amount = x.Amount,
                    Status = ((HistoryWalletStatus)x.Status).ToString(),
                    Time = x.Time.Value.ToString("dd/MM/yyyy HH:mm"),
                    Type = x.Type
                }).OrderByDescending(x => x.Id)
                .ToList();

            return histories;
        }

        public decimal UpdateBalance(decimal? changes, int user_id, bool isFromTran)
        {
            var wallet = _repositoryManager.Wallet.FindByCondition(x => x.UserId == user_id, true).FirstOrDefault();
            if (wallet != null)
            {
                if (wallet.Balance + changes < 0)
                {
                    _repositoryManager.HistoryWallet.Create(new Entities.Models.HistoryWallet
                    {
                        Amount = changes.ToString(),
                        UserId = user_id,
                        IdWallet = wallet.Id,
                        Status = (int)HistoryWalletStatus.Fail,
                        Time = DateTime.UtcNow.AddHours(7),
                        Type = isFromTran ? "Thanh toán đặt sân" : changes < 0 ? "Rút tiền" : "Nạp tiền"
                    });

                    _repositoryManager.SaveAsync().Wait();
                    return -1;
                }
                else
                {
                    wallet.Balance += changes;
                    _repositoryManager.SaveAsync().Wait();

                    _repositoryManager.HistoryWallet.Create(new Entities.Models.HistoryWallet
                    {
                        Amount = changes.ToString(),
                        UserId = user_id,
                        IdWallet = wallet.Id,
                        Status = (int)HistoryWalletStatus.Success,
                        Time = DateTime.UtcNow.AddHours(7),
                        Type = isFromTran ? "Thanh toán đặt sân" : changes < 0 ? "Rút tiền" : "Nạp tiền"
                    });
                    _repositoryManager.SaveAsync().Wait();

                    return wallet.Balance.Value;
                }
            }
            return -2;
        }

        public async Task<Wallet> getWallerByUserId(int userId)
        {
            try
            {
                var userWallet = await _repositoryManager.Wallet.FindByCondition(x => x.UserId == userId, false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (userWallet != null)
                {
                    return userWallet;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
