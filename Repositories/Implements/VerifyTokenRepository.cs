using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    public class VerifyTokenRepository : RepositoryBase<VerifyToken>, IVerifyTokenRepository
    {
        public VerifyTokenRepository(DataContext context) : base(context)
        {

        }
    }
}
