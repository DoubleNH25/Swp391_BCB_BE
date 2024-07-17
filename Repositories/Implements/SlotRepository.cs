using Entities;
using Entities.Models;
using Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implements
{
    internal class SlotRepository : RepositoryBase<Slot>, ISlotRepository
    {
        public SlotRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
