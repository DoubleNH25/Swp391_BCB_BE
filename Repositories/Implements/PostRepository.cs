using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
