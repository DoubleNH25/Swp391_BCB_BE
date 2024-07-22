using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Implements;

namespace BadmintonMatching.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private List<NewImgChat> imgs;
        private readonly IPostServices _postServices;
        public ImageController(IPostServices postServices)
        {
            _postServices = postServices;
        }
        [HttpPost]
        [Route("images")]
        public async Task<IActionResult> AddImages(NewImgChat info)
        {
            var imgs = new NewImgChat
            {
                ImgUrl = await _postServices.HandleImg(info.ImgUrl),
            };
            return Ok(imgs);
        }
    }
}
