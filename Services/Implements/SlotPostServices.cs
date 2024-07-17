using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.EntityFrameworkCore;
using Repositories.Intefaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class SlotPostServices : ISlotPostServices
    {
        private readonly IRepositoryManager _repositoryManager;
        

        public SlotPostServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
           
        }

    }
}
