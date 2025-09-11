using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;   // ✅ PascalCase cho namespace
using Services.Interfaces;

namespace Services.Implements  // ✅ PascalCase cho namespace
{
    // ✅ Class PascalCase, đúng convention
    public class SlotService : ISlotService
    {
        // ✅ Private field: _camelCase
        private readonly IRepositoryManager _repositoryManager;

        // ❌ Sai: private field không có "_" và PascalCase
        // private readonly IRepositoryManager RepositoryManager;

        // ✅ Constructor PascalCase
        public SlotService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        // ✅ Method PascalCase, parameter camelCase
        public void Delete(List<int> slotIds)
        {
            // ✅ Local variable camelCase
            foreach (var slotId in slotIds)
            {
                var slot = _repositoryManager.Slot
                    .FindByCondition(x => x.Id == slotId && !x.IsDeleted, true)
                    .FirstOrDefault();

                if (slot != null)
                {
                    _repositoryManager.Slot.Delete(slot);
                }
            }

            _repositoryManager.SaveAsync().Wait();
        }

        // ❌ Sai: Method viết thường + parameter PascalCase
        // public List<SlotReturnInfo> getavailable(CheckAvailableSlot Info)

        // ✅ Method PascalCase + parameter camelCase
        public List<SlotReturnInfo> GetAvailable(CheckAvailableSlot request)
        {
            var results = new List<SlotReturnInfo>();

            var post = _repositoryManager.Post
                .FindByCondition(x => x.Id == request.PostId, true)
                .Include(x => x.Slot.Where(y => !y.IsDeleted))
                .FirstOrDefault();

            if (post == null)
            {
                return results;
            }

            if (post.UserIdTo == request.UserId)
            {
                throw new FieldAccessException();
            }

            var availableInfos = new List<SlotInfo>();
            foreach (var slotInfo in post.SlotsInfo.Split(';'))
            {
                if (!string.IsNullOrEmpty(slotInfo))
                {
                    availableInfos.Add(new SlotInfo(slotInfo));
                }
            }

            foreach (var inputInfo in request.SlotsInfo)
            {
                var inputDate = inputInfo.DateRegis.ToString("dd/MM/yyyy");
                var createdSlot = availableInfos
                    .Where(x => x.StartTime.Value.ToString("dd/MM/yyyy") == inputDate)
                    .Select(x => x.AvailableSlot)
                    .FirstOrDefault();

                if (createdSlot == 0)
                {
                    results.Add(new SlotReturnInfo
                    {
                        Date = inputDate,
                        Message = "Date not found in post",
                        SlotIds = null
                    });
                    continue;
                }

                var subscribedCount = post.Slot
                    .Where(x => x.ContentSlot == inputDate)
                    .Count();

                if (createdSlot - subscribedCount - inputInfo.NumSlots >= 0)
                {
                    var slotIds = new List<int>();

                    for (var i = 0; i < inputInfo.NumSlots; i++)
                    {
                        var slot = new Slot
                        {
                            ContentSlot = inputDate,
                            IdPost = request.PostId,
                            UserId = request.UserId,
                            Price = availableInfos
                                .Where(x => x.StartTime.Value.ToString("dd/MM/yyyy") == inputDate)
                                .FirstOrDefault().Price
                        };

                        _repositoryManager.Slot.Create(slot);
                        _repositoryManager.SaveAsync().Wait();
                        slotIds.Add(slot.Id);
                    }

                    if (slotIds.Any())
                    {
                        results.Add(new SlotReturnInfo
                        {
                            Date = inputDate,
                            Message = "Success to create",
                            SlotIds = slotIds
                        });
                    }
                }
                else
                {
                    results.Add(new SlotReturnInfo
                    {
                        Date = inputDate,
                        Message = $"Not enough slot (Available: {createdSlot - subscribedCount})",
                        SlotIds = null
                    });
                }
            }

            return results;
        }
    }
}