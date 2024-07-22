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

    }
}
