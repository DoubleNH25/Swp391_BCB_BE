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

    }
}
