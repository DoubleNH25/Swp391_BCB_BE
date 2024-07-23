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
        [HttpPut]
        [Route("update_post")]
        public async Task<IActionResult> UpdatePost(UpdatetPost post)
        {

            var res = await _postServices.UpdatePost(post);
            if (res == -1)
            {
                return Ok(new SuccessObject<object> { Message = "Bài đăng không tồn tại !" });
            }
            else if (res == 1)
            {
                return Ok(new SuccessObject<object> { Data = new { post.idPost }, Message = "Cập nhập thành công !" });
            }
            else if (res == 2)
            {
                return Ok(new SuccessObject<object> { Message = "Ngày bắt đầu không được phải sau ngày kết thúc của vị trí hiện tại !" });
            }
            else
            {
                return Ok(new SuccessObject<object> { Message = "Lưu thất bại !" });
            }
        }
        [HttpGet]
        [Route("{user_id}/managed_all_post")]
        public IActionResult GetPostByPlayGround(int user_id)
        {
            if (!_userServices.ExistUserId(user_id))
            {
                return Ok(new SuccessObject<List<PostInfomation?>> { Message = "Không thể tìm thấy người dùng !" });
            }

            List<PostInfomation> res = new List<PostInfomation>();

            if (_userServices.IsAdmin(user_id))
            {
                res = _postServices.GetManagedPostAdmin(user_id);
            }
            else
            {
                res = _postServices.GetManagedPost(user_id);
            }


            return Ok(new SuccessObject<List<PostInfomation>> { Data = res, Message = Message.SuccessMsg });
        }
        [HttpGet]
        [Route("{post_id}/details")]
        public async Task<IActionResult> GetDetailPost(int post_id)
        {
            var res = await _postServices.GetPostDetail(post_id);
            return Ok(new SuccessObject<PostDetail> { Data = res, Message = Message.SuccessMsg });
        }

        [HttpGet]
        [Route("{post_id}/status")]
        public async Task<IActionResult> GetStatuslPost(int post_id)
        {
            var res = await _postServices.GetPostStatus(post_id);
            return Ok(new SuccessObject<List<SlotCheckStatus>> { Data = res, Message = Message.SuccessMsg });
        }

        [HttpGet]
        [Route("{user_id}/post_suggestion")]
        public IActionResult GetListOptionalPost()
        {
            var res = _postServices.GetListOptionalPost();
            return Ok(new SuccessObject<List<PostOptional>> { Data = res, Message = Message.SuccessMsg });
        }
    }
}
