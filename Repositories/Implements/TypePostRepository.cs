using Entities;
using Entities.Models;
using Repositories.Intefaces;

namespace Repositories.Implements
{
    internal class TypePostRepository : RepositoryBase<TypePost>, ITypePostRepository
    {
        public TypePostRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
