using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BadmintonMatching.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationServices _notificationServices;

        public NotificationController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }
        [HttpPut]
        [Route("readed")]
        public async Task<IActionResult> ReadedNotification(ReadedNoti info)
        {
            if (await _notificationServices.ReadedAll(info))
            {
                return Ok(new SuccessObject<object> { Data = true, Message = Message.SuccessMsg });
            }
            return Ok(new SuccessObject<object> { Message = "Fail to update" });
        }
    }
}