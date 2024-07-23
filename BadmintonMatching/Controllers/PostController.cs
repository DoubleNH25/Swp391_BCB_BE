using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Intefaces;
using Services.Implements;
using Services.Interfaces;
using System.Text.Json.Nodes;

namespace BadmintonMatching.Controllers
{

    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private List<string> PlaceList = new List<string>
        {

        };
        private readonly IPostServices _postServices;
        private readonly IUserServices _userServices;
        private readonly INotificationServices _notificationServices;
        private readonly IRepositoryManager _repositoryManager;


        public PostController(IPostServices postServices, IUserServices userServices,
            INotificationServices notificationServices, IRepositoryManager repositoryManager
            )
        {
            _postServices = postServices;
            _userServices = userServices;
            _notificationServices = notificationServices;
            _repositoryManager = repositoryManager;

        }
        [HttpPost]
        [Route("create_by/{user_id}")]
        public async Task<IActionResult> CreatePost(int user_id, NewPostInfo info)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                Ok(new SuccessObject<object> { Message = "Không thể tìm thấy người dùng !" });
            }
            var postId = await _postServices.CreatePost(user_id, info);
            if (postId == -1)
            {
                return Ok(new SuccessObject<object> { Message = "Chuỗi base64 không hợp lệ !" });
            }
            else if (postId != 0)
            {
                var subIds = await _userServices.GetSubcribeUser(user_id);
                await _notificationServices.SendNotification(subIds, "Hoạt động mới", "Một người mà bạn đăng kí vừa đăng bài", NotificationType.Post, postId);

                return Ok(new SuccessObject<object> { Data = new { PostId = postId }, Message = Message.SuccessMsg });
            }
            else
            {
                return Ok(new SuccessObject<object> { Message = "Lưu thất bại !" });
            }

        }
    }
}
