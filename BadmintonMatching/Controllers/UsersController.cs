using System.Net.WebSockets;
using System.Web;
using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using Services.Implements;
using Services.Interfaces;

namespace BadmintonMatching.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IJwtSupport _jwtServices;
        private readonly INotificationServices _notificationServices;

        public UsersController(IUserServices userServices, IJwtSupport jwtServices, INotificationServices notificationServices)
        {
            _userServices = userServices;
            _jwtServices = jwtServices;
            _notificationServices = notificationServices;
        }

        [HttpPost]
        [Route("email_login")]

    }
}
