using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class HistoryWalletRepository : RepositoryBase<HistoryWallet>, IHistoryWalletRepository
    {
        public HistoryWalletRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
