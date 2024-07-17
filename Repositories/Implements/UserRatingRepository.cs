using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class UserRatingRepository : RepositoryBase<UserRating>, IUserRatingRepository
    {
        public UserRatingRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
