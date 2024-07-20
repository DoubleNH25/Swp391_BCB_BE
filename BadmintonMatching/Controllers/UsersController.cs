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

        [HttpPost]
        [Route("{user_id}/playing_level")]
        public IActionResult AddPlayingLevel(int user_id, NewPlayingLevel info)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy người dùng !" });
            }
            if (info.Point > 0)
            {
                _userServices.AddPlayingLevel(user_id, info);
            }
            return Ok(new SuccessObject<object> { Message = "Cấp độ chơi đã được lưu !", Data = true });
        }

        [HttpPost]
        [Route("{user_id}/playing_way")]
        public IActionResult AddPlayingWay(int user_id, NewPlayingWay info)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy người dùng !" });
            }
            if (info.PlayingWays != null)
            {
                _userServices.AddPlayingWay(user_id, info);
            }
            return Ok(new SuccessObject<object> { Message = "Cách chơi đã được lưu !", Data = true });
        }

        [HttpGet]
        [Route("{user_id}/player_suggestion")]
        [ProducesResponseType(typeof(SuccessObject<List<UserSuggestion>>), 200)]
        public IActionResult GetPlayerSuggestion(int user_id)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<List<UserSuggestion?>> { Message = "Không tìm thấy người dùng !" });
            }
            var areas = _userServices.GetUserAreas(user_id);
            var res = new List<UserSuggestion>();
            if (areas.Count() > 0)
            {
                res = _userServices.FindUserByArea(areas);
            }
            if (res.Count() < 9)
            {
                int skill = _userServices.GetUserSkill(user_id);
                if (skill > 0)
                {
                    res = _userServices.FindUserBySkill(skill, res);
                }
            }
            if (res.Count() < 9)
            {
                List<string> ways = _userServices.GetUserPlayWay(user_id);
                if (ways.Count > 0)
                {
                    res = _userServices.FindUserByPlayWays(ways, res);
                }
            }
            return Ok(new SuccessObject<List<UserSuggestion>> { Data = res, Message = Message.SuccessMsg });
        }

        [HttpGet]
        [Route("{email}/verify_token")]
        public async Task<IActionResult> GetVerifyToken(string email)
        {
            if (!_userServices.IsUserExist(email))
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy người dùng !" });
            }

            await _userServices.SendEmailAsync(email);
            return Ok(new SuccessObject<object> { Data = true, Message = "Gửi thư thành công !" });
        }

        [HttpPost]
        [Route("verify_token")]
        public IActionResult VerifyToken(UserVerifyToken info)
        {
            if (!_userServices.IsUserExist(info.Email))
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy người dùng !" });
            }

            var success = _userServices.CheckRemoveVefToken(info);

            return Ok(success ? new SuccessObject<object> { Message = "Xác minh thành công !", Data = true }
            : new SuccessObject<object> { Message = "Mã không hợp lệ !" });
        }

        [HttpPut]
        [Route("{email}/new_pass")]
        public IActionResult ChangePassword(string email, UpdatePassword info)
        {
            if (!_userServices.IsUserExist(email))
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy người dùng !" });
            }

            if (info.NewPassword != info.ReEnterPassword)
            {
                return Ok(new SuccessObject<object> { Message = "Xác minh mật khẩu không khớp  !" });
            }

            var success = _userServices.UpdatePassword(email, info);

            return Ok(success ? new SuccessObject<object> { Message = "Cập nhật thành công", Data = true } : new SuccessObject<object> { Message = "Cập nhật thất bại" });
        }

        [HttpGet]
        [Route("{user_id}/comments")]
        [ProducesResponseType(typeof(SuccessObject<List<CommentInfos>>), 200)]
        public IActionResult GetComments(int user_id)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<List<CommentInfos?>> { Message = "Không tìm thấy người dùng !" });
            }

            var res = _userServices.GetComments(user_id);

            return Ok(new SuccessObject<List<CommentInfos>> { Data = res, Message = Message.SuccessMsg });
        }

        [HttpPost]
        [Route("{user_id}/comments/{user_id_receive_comment}")]
        public IActionResult AddComment(int user_id, int user_id_receive_comment, AddCommentToUser comment)
        {
            if (!_userServices.ExistUserId(user_id) || !_userServices.ExistUserId(user_id_receive_comment))
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy người dùng !" });
            }

            int commentId = _userServices.SaveComment(user_id, user_id_receive_comment, comment);

            return Ok(commentId > 0 ? new SuccessObject<object> { Message = "Cập nhật thành công", Data = true }
            : new SuccessObject<object> { Message = "Cập nhật thất bại" });
        }

        [HttpGet]
        [Route("{user_id}/banded_users")]
        [ProducesResponseType(typeof(SuccessObject<List<BandedUsers>>), 200)]
        public IActionResult GetBandedUsers(int user_id)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<List<BandedUsers?>> { Message = "Không tìm thấy người dùng !" });
            }

            List<BandedUsers> bandedLs = _userServices.GetBandedUsers(user_id);

            return Ok(new SuccessObject<List<BandedUsers>> { Data = bandedLs, Message = Message.SuccessMsg });
        }

        [HttpPut]
        [Route("{user_id}/ban_unban/{user_effect}")]
        public IActionResult BanUnban(int user_id, int user_effect)
        {
            bool updateSuccess = _userServices.BanUnband(user_id, user_effect);

            return Ok(updateSuccess ? new SuccessObject<object> { Message = "Cập nhật thành công", Data = true }
            : new SuccessObject<object> { Message = "Cập nhật thất bại" });
        }

        [HttpGet]
        [Route("managed/{user_id}")]
        [ProducesResponseType(typeof(SuccessObject<List<UserManaged>>), 200)]
        public IActionResult GetAllUserForManaged(int user_id)
        {
            if (!_userServices.IsAdminAndStaff(user_id))
            {
                return Ok(new SuccessObject<List<UserManaged?>> { Message = "Không phải quản trị viên hoặc nhân viên để có được" });
            }

            var users = _userServices.GetUserForManaged();

            return Ok(new SuccessObject<List<UserManaged>> { Data = users, Message = Message.SuccessMsg });
        }

        [HttpPut]
        [Route("{admin_id}/banded/{user_id}")]
        public IActionResult BanUnbanUser(int admin_id, int user_id)
        {

            try
            {
                bool isBanded = _userServices.BanUnbandLogin(user_id);

                return Ok(new SuccessObject<object> { Data = new { status = isBanded ? "Banded" : "Unbaded" }, Message = Message.SuccessMsg });
            }
            catch (NotImplementedException)
            {
                return Ok(new SuccessObject<object> { Message = "Không tìm thấy id người dùng !" });
            }
        }


        #region Get User Profile
        [HttpGet]
        [Route("{user_id}/profile")]
        [ProducesResponseType(typeof(SuccessObject<UserProfile>), 200)]
        public IActionResult GetUserProfile(int user_id)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<UserProfile?> { Message = "Không tìm thấy người dùng !" });
            }
            var res = _userServices.GetUserProfileSetting(user_id);
            res.Helpful = _userServices.GetHelpful(user_id);
            res.Friendly = _userServices.GetFriendly(user_id);
            res.Trusted = _userServices.GetTrusted(user_id);
            res.LevelSkill = _userServices.GetLevelSkill(user_id);

            return Ok(new SuccessObject<UserProfile> { Data = res, Message = Message.SuccessMsg });
        }
        #endregion

        #region Get all user
        [HttpGet]
        [Route("GetListUser")]
        [ProducesResponseType(typeof(SuccessObject<List<User>>), 200)]
        public async Task<IActionResult> GetAllAccount()
        {
            var res = await _userServices.GetAllAccount();
            return Ok(new SuccessObject<List<User>> { Data = res, Message = Message.SuccessMsg });
        }
        #endregion

        #region Update User's Profile
        /// <summary>
        /// Update user profile (Role: Customer)
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{user_id}")]
        public async Task<IActionResult> UpdateUserProfile(UpdateProfileUser param, int user_id, bool trackChanges)
        {
            try
            {
                var data = await _userServices.UpdateProfile(user_id, param, trackChanges);
                return Ok(new SuccessObject<object> { Data = data, Message = Message.SuccessMsg });
            }
            catch
            {
                return Ok(new SuccessObject<object> { Message = "Invalid base 64 string" });
            }
            //await _repository.SaveAsync();
            //return Ok();
        }

        #endregion



        #region Get User Profile
        [HttpGet]
        [Route("user_id")]
        [ProducesResponseType(typeof(SuccessObject<SelfProfile>), 200)]
        public IActionResult GetSelfProfile(int user_id)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<SelfProfile?> { Message = "Không tìm thấy người dùng !" });
            }
            var res = _userServices.GetSelfProfile(user_id);
            return Ok(new SuccessObject<SelfProfile> { Data = res, Message = Message.SuccessMsg });
        }
        #endregion

        [HttpPut]
        [Route("{user_id}/sub_unsub/{target_id}")]
        public async Task<IActionResult> SubUnsub(int user_id, int target_id)
        {
            var res = await _userServices.SubUnSub(user_id, target_id);
            switch (res)
            {
                case -2:
                    {
                        return Ok(new SuccessObject<object?> { Message = "Không tìm thấy người dùng !" });
                    }
                case -1:
                    {
                        return Ok(new SuccessObject<object?> { Message = "Không thể tìm thấy người dùng mục tiêu !" });
                    }
                default:
                    {
                        var addTxt = res == 0 ? "Unsubcribe" : "Subcribe";
                        if (res == 1)
                        {
                            await _notificationServices.SendNotification(target_id, "Tương tác", "Bạn đã có một lượt đăng kí", NotificationType.User, user_id);
                        }
                        return Ok(new SuccessObject<object?> { Data = true, Message = $"{addTxt} success!" });
                    }
            }
        }

        [HttpPut]
        [Route("{user_id}/check_sub/{usertarget_id}")]
        public async Task<IActionResult> CheckSub(int user_id, int usertarget_id)
        {
            var checkSub = await _userServices.CheckSub(user_id, usertarget_id);

            if (checkSub)
            {

                return Ok(new SuccessObject<object?> { Data = new { subed = true }, Message = "userId: " + user_id + "subed userId: " + usertarget_id });
            }
            return Ok(new SuccessObject<object?> { Message = "userId :" + user_id + "not sub userId: " + usertarget_id });
        }





        [HttpPut]
        [Route("{user_id}/setting_password")]
        public async Task<IActionResult> SettingPassword(int user_id, SettingPasswordRequest info)
        {
            var res = await _userServices.SettingPassword(user_id, info);
            if (res == 1)
            {
                return Ok(new SuccessObject<object?> { Data = true, Message = $"Cập nhật thành công!" });
            }
            else if (res == 0)
            {
                return Ok(new SuccessObject<object?> { Message = "Mật khẩu cũ không hợp lệ !" });
            }
            else if (res == -1)
            {
                return Ok(new SuccessObject<object?> { Message = "Nhập lại mật khẩu sai !" });
            }
            else if (res == -2)
            {
                return Ok(new SuccessObject<object?> { Message = "Người dùng không tồn tại !" });
            }
            else
            {
                return Ok(new SuccessObject<object?> { Message = "Cập nhật thất bại" });
            }
        }

        #region Detail user by id
        [HttpGet]
        [Route("{user_id}/getdetail")]
        public IActionResult GetDetailUser(int user_id)
        {
            var userdetail = _userServices.GetDetailUser(user_id);
            return Ok(new SuccessObject<UserInformation> { Data = userdetail, Message = Message.SuccessMsg });
        }
        #endregion

        [HttpPost]
        [Route("rating_to")]
        public async Task<IActionResult> RatingUser(RatingUserInfo info)
        {
            if (await _userServices.Rating(info))
            {
                return Ok(new SuccessObject<object> { Message = Message.SuccessMsg, Data = true });
            }
            else
            {
                return Ok(new SuccessObject<object> { Message = "Đánh giá thất bại !" });
            }
        }

        [HttpGet]
        [Route("{user_id}/notification")]
        public async Task<IActionResult> GetNotifications(int user_id)
        {
            var notifications = await _userServices.GetNotifications(user_id);
            return Ok(new SuccessObject<List<NotificationReturn>> { Data = notifications, Message = Message.SuccessMsg });
        }


        [HttpGet]
        [Route("admin/{admin_id}/user/{user_id}/detail")]
        public async Task<IActionResult> GetManagedProfile(int admin_id, int user_id)
        {
            try
            {
                if (!_userServices.IsAdmin(admin_id))
                    throw new Exception("Bạn không có quyền truy cập !");

                var res = await _userServices.GetManagedProfile(user_id);
                return Ok(new SuccessObject<ManagedDetailUser> { Data = res, Message = Message.SuccessMsg });
            }
            catch (Exception ex)
            {
                return Ok(new SuccessObject<object> { Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{user_id}/to/{role_id}/by/{admin_id}")]
        public async Task<IActionResult> UpdateRole(int user_id, int role_id, int admin_id)
        {
            try
            {
                if (!_userServices.IsAdmin(admin_id))
                    throw new Exception("Bạn không có quyền truy cập !");

                if (await _userServices.UpdateRole(user_id, (UserRole)role_id))
                    return Ok(new SuccessObject<object> { Data = true, Message = Message.SuccessMsg });
                else
                {
                    throw new Exception("Cập nhật thất bại !");
                }
            }
            catch (Exception ex)
            {
                return Ok(new SuccessObject<object> { Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("notice/to/{user_id}")]
        public async Task<IActionResult> SendNotices(int user_id, NoticesRequest info)
        {
            await _notificationServices.SendNotification(user_id, "Nhắc nhở từ Admin", info.Message, NotificationType.User, user_id);
            return Ok(new SuccessObject<object> { Message = "Nhắc nhở thành công", Data = true });
        }

        [HttpPut]
        [Route("updatePolicy/{user_id}")]
        public async Task<IActionResult> UpdatePolicy(int user_id)
        {
            var isSuccess = await _userServices.UpdatePolicy(user_id);
            if (!isSuccess)
            {
                return Ok(new SuccessObject<object> { Message = "Cập nhật chính sách thất bại ! " });
            }
            return Ok(new SuccessObject<object> { Data = new { user = user_id }, Message = Message.SuccessMsg });
        }
    }
}
