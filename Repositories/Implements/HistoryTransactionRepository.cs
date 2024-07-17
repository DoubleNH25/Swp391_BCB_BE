using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class HistoryTransactionRepository : RepositoryBase<HistoryTransaction>, IHistoryTransactionRepository
    {
        public HistoryTransactionRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
