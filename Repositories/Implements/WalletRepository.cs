using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
    {
        public WalletRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
