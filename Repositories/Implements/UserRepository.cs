using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
