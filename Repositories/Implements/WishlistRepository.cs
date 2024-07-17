using Entities;
using Entities.Models;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class WishlistRepository : RepositoryBase<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
