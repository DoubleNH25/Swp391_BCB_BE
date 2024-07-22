using CloudinaryDotNet.Actions;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BadmintonMatching.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IPostServices _postServices;
        private readonly IUserServices _userServices;

        public BlogController(IPostServices postServices, IUserServices userServices)
        {
            _postServices = postServices;
            _userServices = userServices;
        }

        [HttpPost]
        [Route("create_by/{user_id}")]
        public async Task<IActionResult> CreateBlog(int user_id, NewBlogInfo info)
        {
            try
            {
                if (!await _userServices.IsStaff(user_id))
                {
                    throw new Exception("Bạn không có quyền tạo tin tức !");
                }

                if (await _postServices.CreateBlog(user_id, info))
                {
                    return Ok(new SuccessObject<object> { Message = Message.SuccessMsg, Data = true });
                }
                else
                {
                    return Ok(new SuccessObject<object> { Message = "Tạo tin tức thất bại !" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new SuccessObject<object> { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetBlogs(int userId)
        {
            var res = await _postServices.GetAllBlogs(userId);
            return Ok(new SuccessObject<List<BlogInList>> { Data = res, Message = Message.SuccessMsg });
        }

        [HttpGet]
        [Route("{blog_id}/details")]
        public async Task<IActionResult> GetBlogDetail(int blog_id)
        {
            try
            {
                var detail = await _postServices.GetBlogDetail(blog_id);
                //detail.CanDelete = _userServices.IsAdminAndStaff(user_id);
                return Ok(new SuccessObject<BlogDetail> { Data = detail, Message = Message.SuccessMsg });
            }
            catch (Exception ex)
            {
                return Ok(new SuccessObject<object> { Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{blog_id}/by/{user_id}")]
        public async Task<IActionResult> DeleteBlog(int user_id, int blog_id)
        {
            try
            {
                if (!_userServices.IsAdminAndStaff(user_id))
                {
                    throw new Exception("Bạn không có quyền để xóa !");
                }

                if (await _postServices.DeleteBlogAsync(blog_id))
                {
                    return Ok(new SuccessObject<object> { Message = Message.SuccessMsg, Data = true });
                }
                else
                {
                    return Ok(new SuccessObject<object> { Message = "Xóa tin tức thất bại !" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new SuccessObject<object> { Message = ex.Message });
            }
        }
    }
}
