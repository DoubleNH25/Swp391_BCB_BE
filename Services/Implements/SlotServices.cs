using Entities.Models;
using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.EntityFrameworkCore;
using Repositories.Intefaces;
using Services.Interfaces;

namespace Services.Implements
{
    public class SlotServices : ISlotServices
    {
        private readonly IRepositoryManager _repositoryManager;


        public SlotServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;

        }

        public void Delete(List<int> lsSlot)
        {
            //    foreach (var slotId in lsSlot)
            //    {
            //        var slot = _repositoryManager.Slot.FindByCondition(x => x.Id == slotId && !x.IsDeleted, true).FirstOrDefault();
            //        if(slot != null)
            //        {
            //            _repositoryManager.Slot.Delete(slot);
            //        }
            //    }
            //    _repositoryManager.SaveAsync().Wait();
        }

        public List<SlotReturnInfo> GetAvailable(CheckAvailableSlot info)
        {
            var res = new List<SlotReturnInfo>();

            //var postSubcript = _repositoryManager.Post
            //    .FindByCondition(x => x.Id == info.PostId, true)
            //    .Include(x => x.Slot.Where(y => !y.IsDeleted)).FirstOrDefault();

            //if (postSubcript != null)
            //{
            //    if(postSubcript.UserIdTo == info.UserId)
            //    {
            //        throw new FieldAccessException();
            //    }
            //    var lsAvaiInfo = new List<SlotInfo>();
            //    foreach (var infoSlot in postSubcript.SlotsInfo.Split(';'))
            //    {
            //        if (infoSlot != string.Empty)
            //        {
            //            lsAvaiInfo.Add(new SlotInfo(infoSlot));
            //        }
            //    }
            //    foreach (var inputInfo in info.SlotsInfo)
            //    {
            //        var inputDate = inputInfo.DateRegis.ToString("dd/MM/yyyy");
            //        var createSlot = lsAvaiInfo
            //            .Where(x => x.StartTime.Value.ToString("dd/MM/yyyy") == inputDate)
            //            .Select(x => x.AvailableSlot).FirstOrDefault();
            //        if(createSlot == 0)
            //        {
            //            res.Add(new SlotReturnInfo
            //            {
            //                Date = inputDate,
            //                Message = $"Date not found in post",
            //                SlotIds = null
            //            });
            //            continue;
            //        }
            //        var slotSubcripted = postSubcript.Slot.Where(x => x.ContentSlot == inputDate).Count();
            //        if (createSlot - slotSubcripted - inputInfo.NumSlots >= 0)
            //        {
            //            var slotIds = new List<int>();
            //            for (var i = 0; i < inputInfo.NumSlots; i++)
            //            {
            //                var slot = new Slot
            //                {
            //                    ContentSlot = inputInfo.DateRegis.ToString("dd/MM/yyyy"),
            //                    IdPost = info.PostId,
            //                    UserId = info.UserId,
            //                    Price = lsAvaiInfo.Where(x => x.StartTime.Value.ToString("dd/MM/yyyy") == inputDate).FirstOrDefault().Price
            //                };
            //                _repositoryManager.Slot.Create(slot);
            //                _repositoryManager.SaveAsync().Wait();
            //                slotIds.Add(slot.Id);
            //            }
            //            if (slotIds.Count() > 0)
            //            {
            //                res.Add(new SlotReturnInfo
            //                {
            //                    Date = inputDate,
            //                    Message = "Success to create",
            //                    SlotIds = slotIds
            //                });
            //            }
            //        }
            //        else
            //        {
            //            res.Add(new SlotReturnInfo
            //            {
            //                Date = inputDate,
            //                Message = $"Not enough slot for subcript(Available slot is: {createSlot - slotSubcripted})",
            //                SlotIds = null
            //            });
            //        }
            //    }
            //}
            return res;
        }
    }
}
