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
        public IActionResult GetUserByEmail(LoginInformation info)
        {
            if (!_userServices.IsUserExist(info.Email))
            {
                return Ok(new SuccessObject<object> { Message = "Tài khoản không tồn tại" });
            }
            var userInfo = _userServices.GetExistUser(info);

            if (userInfo.Id == -1)
            {
                return Ok(new SuccessObject<object> { Message = "Tài khoản của bạn đã bị khóa" });
            }
            else if (userInfo.Id == 0)
            {
                return Ok(new SuccessObject<object> { Message = "Tài khoản hoặc mật khẩu không đúng" });
            }
            return Ok(new SuccessObject<UserInformation> { Data = userInfo, Message = Message.SuccessMsg });
        }


        [HttpGet]
        [Route("{month}${year}/report_income_inMonth")]
        public async Task<IActionResult> GetReportIncomeInMonth(string month, string year)
        {
            var reportIncomeModel = _userServices.GetIncomeByInMonth(month, year);
            return Ok(new SuccessObject<ReportIncomeModel> { Data = reportIncomeModel, Message = Message.SuccessMsg });
        }

        [HttpGet]
        [Route("{startDate}&{endDate}/report_income_Month")]
        public async Task<IActionResult> GetReportIncomeMonth(string startDate, string endDate)


        {
            startDate = HttpUtility.UrlDecode((startDate));
            endDate = HttpUtility.UrlDecode((endDate));

            var reportIncomeModel = _userServices.GetIncomeByMonth(startDate, endDate);
            return Ok(new SuccessObject<ReportIncomeModel> { Data = reportIncomeModel, Message = Message.SuccessMsg });
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegistUser(RegisInfomation info)
        {
            if (info.Password != info.ReEnterPass)
            {
                return Ok(new SuccessObject<object> { Message = "Mật khẩu không trùng khớp" });
            }

            if (_userServices.IsUserExist(info.Email))
            {
                return Ok(new SuccessObject<object> { Message = "Email này đã tồn tại" });
            }

            var userId = _userServices.RegistUser(info);
            return Ok(new SuccessObject<object> { Data = new { UserId = userId }, Message = Message.SuccessMsg });
        }

        [HttpPost]
        [Route("{user_id}/playing_area")]
        public IActionResult AddPlayingArea(int user_id, NewPlayingArea info)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy người dùng !" });
            }
            if (info.ListArea != null)
            {
                _userServices.AddPlayingArea(user_id, info);
            }
            return Ok(new SuccessObject<object> { Message = "Khu vực chơi đã được lưu !", Data = true });
        }
    }
}
