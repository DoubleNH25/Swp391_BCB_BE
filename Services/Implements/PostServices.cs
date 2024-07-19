using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Intefaces;
using Services.Interfaces;
using System.Data;
using System.Globalization;
using System.Net.WebSockets;
using System.Xml.Linq;


using Accord.Statistics.Analysis;
using Accord.Statistics.Filters;
using Accord.IO;
using Accord.Math;
using Accord.MachineLearning;
using Microsoft.AspNetCore.Mvc.Filters;
using Accord.Math.Geometry;
using static Entities.ResponseObject.SlotCheckStatus;

namespace Services.Implements
{
    public enum PostType
    {
        MatchingPost = 1,
        Blog = 2
    }

    public class TempTransaction
    {
        public int Id { get; set; }
        public decimal? MoneyTrans { get; set; }
        public int? Status { get; set; }
        public List<string> ContentSlots { get; set; }
    }


    public class PostServices : IPostServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IUserServices _userServices;
        private readonly ISlotServices _slotServices;
        private string[] timeSlots = {
            "6h-8h",
            "8h-10h",
            "10h-12h",
            "12h-14h",
            "14h-16h",
            "16h-18h",
            "18h-20h",
            "20h-22h"
        };


        public PostServices(IRepositoryManager repositoryManager,
                            IUserServices userServices, ISlotServices slotServices)
        {
            _repositoryManager = repositoryManager;
            _userServices = userServices;
            _slotServices = slotServices;


        }

        public async Task<string> HandleImg(string base64encodedstring)
        {
            var revertbase64 = base64encodedstring.Replace("data:image/jpeg;base64,", "");
            try
            {
                var bytes = Convert.FromBase64String(revertbase64);
                var contents = new StreamContent(new MemoryStream(bytes));
                Account account = new Account(
                    "dgx21lq5x",
                    "282428924532382",
                    "0WvIKDeB9cy-foUumrFN1dab664");
                Cloudinary cloudinary = new Cloudinary(account);

                string publicId = Guid.NewGuid().ToString();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(publicId, new MemoryStream(bytes)),
                    PublicId = publicId
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return uploadResult.SecureUrl.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }

        public async Task<int> CreatePost(int user_id, NewPostInfo info)
        {


            var urls = "";
            foreach (var url in info.ImgUrls)
            {
                try
                {
                    urls += $"{await HandleImg(url)};";
                }
                catch
                {
                    return -1;
                }
            }

            try
            {
                var newPost = new Post
                {
                    CategorySlot = info.CategorySlot,
                    Title = info.Title,
                    AddressSlot = info.Address,
                    ContentPost = info.Description,
                    SavedDate = DateTime.UtcNow.AddHours(7),
                    ImgUrl = await HandleImg(info.HighlightUrl),
                    ImageUrls = info.ImgUrls[1],
                    IdUserTo = user_id,
                    IdType = (int)PostType.MatchingPost,
                    Status = true
                };

                _repositoryManager.Post.Create(newPost);
                await _repositoryManager.SaveAsync();


                foreach (String slotDate in info.SlotInfor.TimeSlot)
                {

                    foreach (String timeSlot in timeSlots)
                    {
                        SlotPost slot = new SlotPost();
                        slot.IdPost = newPost.Id;
                        slot.ContextPost = timeSlot;
                        slot.SlotDate = slotDate;
                        slot.SlotPrice = (decimal)info.SlotInfor.Price;
                        slot.Status = (int)SlotStatus.Watting;
                        _repositoryManager.SlotPost.Create(slot);

                    }
                }
                await _repositoryManager.SaveAsync();

                return newPost.Id;
            }
            catch
            {
                return -1;
            }
        }



        public async Task<bool> DeletePostAsync(int post_id)
        {
            var post = await _repositoryManager.Post.FindByCondition(x => x.Id == post_id && !x.IsDeleted && x.IdType == (int)PostType.MatchingPost, true).FirstOrDefaultAsync();
            if (post != null)
            {
                post.IsDeleted = true;
                await _repositoryManager.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteBlogAsync(int post_id)
        {
            var post = await _repositoryManager.Post.FindByCondition(x => x.Id == post_id && !x.IsDeleted && x.IdType == (int)PostType.Blog, true).FirstOrDefaultAsync();
            if (post != null)
            {
                post.IsDeleted = true;
                await _repositoryManager.SaveAsync();
                return true;
            }
            return false;
        }

        public List<PostOptional> GetListOptionalPost()
        {
            var optList = _repositoryManager.Post.FindByCondition(x => !x.IsDeleted && x.IdType == (int)PostType.MatchingPost, true)
               .OrderByDescending(x => x.SavedDate)
               .ToList();
            var res = new List<PostOptional>();
            foreach (var post in optList)
            {

                res.Add(new PostOptional
                {
                    IdPost = post.Id,
                    Title = post.Title,
                    AddressSlot = post.AddressSlot,
                    ContentPost = post.ContentPost,
                    ImgUrlPost = post.ImgUrl,
                    HighlightUrl = post.ImgUrl,
                    UserId = post.IdUserTo
                });

            }
            return res;

        }
        public async Task<List<PostOptional>> GetAllPost()
        {
            var listpost = await _repositoryManager.Post.FindByCondition(x => !x.IsDeleted && x.IdType == (int)PostType.MatchingPost, false).OrderByDescending(x => x.SavedDate).ToListAsync();
            var res = new List<PostOptional>();
            foreach (var post in listpost)
            {

                res.Add(new PostOptional
                {
                    IdPost = post.Id,
                    Title = post.Title,
                    AddressSlot = post.AddressSlot,
                    ContentPost = post.ContentPost,
                    ImgUrlPost = post.ImgUrl,
                    HighlightUrl = post.ImgUrl,
                    UserId = post.IdUserTo
                });

            }

            return res;
        }



        public List<ListPostByAdmin> GetListPostByAdmin()
        {
            return _repositoryManager.Post.FindByCondition(x => x.IdType == (int)PostType.MatchingPost, true)
                .OrderByDescending(x => x.SavedDate)
                .Select(x => new ListPostByAdmin
                {
                    CreatedDate = x.SavedDate,
                    IdUser = x.IdUserTo,
                    Status = x.Status,
                    TotalViewer = x.TotalViewer.ToString(),
                    IsDeleted = x.IsDeleted
                }).ToList();
        }

        public List<PostInfomation> GetManagedPost(int user_id)
        {
            var res = _repositoryManager.Post.FindByCondition(x => !x.IsDeleted && x.IdType == (int)PostType.MatchingPost && x.IdUserToNavigation.Id == user_id, true)
                .OrderByDescending(x => x.SavedDate)
                .Include(x => x.SlotPosts).Include(x => x.IdUserToNavigation)
                .Select(x => new PostInfomation(x)).ToList();
            foreach (var item in res)
            {
                var post = _repositoryManager.Post.FindByCondition(x => x.Id == item.PostId && !x.IsDeleted && x.IdType == (int)PostType.MatchingPost, true).Include(x => x.SlotPosts).FirstOrDefault();

                var listSlot = _repositoryManager.Slot
        .FindByCondition(x => x.IdPost == item.PostId, false)
        .Include(x => x.IdSlotNavigation)
        .ToList();
                var postSlot = new List<PostSlot>();
                var dates = post.SlotPosts
                    .Where(sp => sp.IdPost == post.Id)
                    .Select(sp => sp.SlotDate)
                    .Distinct()
                    .ToList();
                int slot = 0;
                foreach (var date in dates)
                {

                    var timeSlots = post.SlotPosts
                        .Where(sp => sp.SlotDate == date)
                        .Select(sp => sp.ContextPost)
                        .ToList();
                    foreach (var timeSlot in timeSlots)
                    {
                        var isAdd = false;
                        foreach (var iNaviSlot in listSlot)
                        {

                            var slotAList = _repositoryManager.SlotPost
                                .FindByCondition(x => x.IdSlot == iNaviSlot.IdSlot && x.SlotDate == date && x.IdPost == post.Id && x.ContextPost == timeSlot, false)
                                .ToList();
                            if (slotAList.Count != 0)
                            {
                                isAdd = true;
                                break;
                            }
                        }
                        if (!isAdd)
                        {
                            slot++;
                        }
                    }

                }
                item.AvailableSlot = slot;
                if (dates.Count > 0)
                {
                    string timeslot = dates[0] + " - " + dates[dates.Count() - 1];
                    item.Time = timeslot;
                }
            }
            return res;
        }

        public List<PostInfomation> GetManagedPostAdmin(int user_id)
        {
            var res = _repositoryManager.Post.FindByCondition(x => !x.IsDeleted && x.IdType == (int)PostType.MatchingPost, true)
                .OrderByDescending(x => x.SavedDate)
                .Include(x => x.SlotPosts).Include(x => x.IdUserToNavigation)
                .Select(x => new PostInfomation(x)).ToList();
            foreach (var item in res)
            {
                var post = _repositoryManager.Post.FindByCondition(x => x.Id == item.PostId && !x.IsDeleted && x.IdType == (int)PostType.MatchingPost, true).Include(x => x.SlotPosts).FirstOrDefault();

                var listSlot = _repositoryManager.Slot
        .FindByCondition(x => x.IdPost == item.PostId, false)
        .Include(x => x.IdSlotNavigation)
        .ToList();
                var postSlot = new List<PostSlot>();
                var dates = post.SlotPosts
                    .Where(sp => sp.IdPost == post.Id)
                    .Select(sp => sp.SlotDate)
                    .Distinct()
                    .ToList();
                int slot = 0;
                foreach (var date in dates)
                {

                    var timeSlots = post.SlotPosts
                        .Where(sp => sp.SlotDate == date)
                        .Select(sp => sp.ContextPost)
                        .ToList();
                    foreach (var timeSlot in timeSlots)
                    {
                        var isAdd = false;
                        foreach (var iNaviSlot in listSlot)
                        {

                            var slotAList = _repositoryManager.SlotPost
                                .FindByCondition(x => x.IdSlot == iNaviSlot.IdSlot && x.SlotDate == date && x.IdPost == post.Id && x.ContextPost == timeSlot, false)
                                .ToList();
                            if (slotAList.Count != 0)
                            {
                                isAdd = true;
                                break;
                            }
                        }
                        if (!isAdd)
                        {
                            slot++;
                        }
                    }

                }
                item.AvailableSlot = slot;
                if (dates.Count > 0)
                {
                    string timeslot = dates[0] + " - " + dates[dates.Count() - 1];
                    item.Time = timeslot;
                }
            }
            return res;
        }


        public async Task<PostDetail> GetPostDetail(int id_post)
        {
            var post = await _repositoryManager.Post
                .FindByCondition(x =>
                    x.Id == id_post
                    && !x.IsDeleted
                    && x.IdType == (int)PostType.MatchingPost, true)
                .Include(x => x.SlotPosts)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return new PostDetail();
            }

            post.TotalViewer++;
            await _repositoryManager.SaveAsync();

            var postDetail = new PostDetail(post);
            postDetail.ImageUrls = post.ImageUrls.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

            var listSlot = await _repositoryManager.Slot
                .FindByCondition(x => x.IdPost == id_post, false)
                .Include(x => x.IdSlotNavigation)
                .ToListAsync();

            var postSlot = new List<PostSlot>();
            var today = DateTime.Now.Date;
            var dates = post.SlotPosts
                .Where(sp => sp.IdPost == post.Id && DateTime.ParseExact(sp.SlotDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= today)
                .Select(sp => sp.SlotDate)
                .Distinct()
                .ToList();

            foreach (var date in dates)
            {
                var ps = new PostSlot { DateSlot = date };
                var timeSlots = post.SlotPosts
                    .Where(sp => sp.SlotDate == date)
                    .Select(sp => sp.ContextPost)
                    .ToList();
                foreach (var timeSlot in timeSlots)
                {
                    var isAdd = false;
                    foreach (var iNaviSlot in listSlot)
                    {

                        var slotAList = await _repositoryManager.SlotPost
                            .FindByCondition(x => x.IdSlot == iNaviSlot.IdSlot && x.SlotDate == date && x.IdPost == post.Id && x.ContextPost == timeSlot, false)
                            .ToListAsync();
                        if (slotAList.Count != 0)
                        {
                            isAdd = true;
                            break;
                        }
                    }
                    if (!isAdd)
                    {
                        ps.slot.Add(timeSlot);
                    }
                }
                postSlot.Add(ps);
            }
            postDetail.postSlot = postSlot;
            return postDetail;
        }

        public async Task<List<JoinedPost>> GetJoined(int user_id)
        {
            var res = new List<JoinedPost>();

            var transactions = await _repositoryManager.Transaction
                .FindByCondition(x => x.UserId == user_id, false).Include(x => x.Slots).ThenInclude(x => x.IdSlotNavigation).ToListAsync();

            if (transactions == null || transactions.Count == 0)
            {
                return res;
            }

            foreach (var item in transactions)
            {
                var post = await _repositoryManager.Post.FindByCondition(x => x.Id == item.Idpost && !x.IsDeleted, false).Include(x => x.SlotPosts).FirstOrDefaultAsync();

                if (post == null)
                    continue;

                var bookedInfos = new List<PostSlot>();
                foreach (var infoStr in item.Slots)
                {
                    var existingSlot = bookedInfos.FirstOrDefault(slot => slot.DateSlot == infoStr.IdSlotNavigation.SlotDate);
                    if (existingSlot != null)
                    {
                        existingSlot.slot.Add(infoStr.IdSlotNavigation.ContextPost);
                    }
                    else
                    {
                        var newSlot = new PostSlot
                        {
                            DateSlot = infoStr.IdSlotNavigation.SlotDate,
                            slot = new List<string> { infoStr.IdSlotNavigation.ContextPost }
                        };
                        bookedInfos.Add(newSlot);
                    }
                }



                Dictionary<TransactionStatus, string> vietnameseStatus = CreateVietnameseStatusDictionary();

                res.Add(new JoinedPost
                {
                    AreaName = post.AddressSlot,
                    MoneyPaid = item.MoneyTrans,
                    TransacionId = item.Id,
                    BookedInfos = bookedInfos,
                    PostId = post.Id,
                    Status = vietnameseStatus[(TransactionStatus)item.Status],
                    PostTitle = post.Title,
                    CoverImage = post.ImgUrl,

                });
            }
            return res;
        }
        public static Dictionary<TransactionStatus, string> CreateVietnameseStatusDictionary()
        {
            Dictionary<TransactionStatus, string> vietnameseStatus = new Dictionary<TransactionStatus, string>
        {

            { TransactionStatus.Booked, "Đặt sân thành công !" },
           { TransactionStatus.Canceled, "Đã hủy !" }
        };

            return vietnameseStatus;
        }

        public async Task<bool> CreateBlog(int user_id, NewBlogInfo info)
        {
            var urls = "";
            foreach (var url in info.ImgUrls)
            {
                try
                {
                    urls += $"{await HandleImg(url)};";
                }
                catch
                {
                    return false;
                }
            }
            var post = new Post
            {
                Title = info.Title,
                ContentPost = info.Description,
                IdUserTo = user_id,
                IdType = (int)PostType.Blog,
                SavedDate = DateTime.UtcNow.AddHours(7),
                IsDeleted = false,
                ImageUrls = urls,
                AddressSlot = info.Summary,
                ImgUrl = await HandleImg(info.HighlightImg),
                TotalViewer = 50,
            };

            _repositoryManager.Post.Create(post);
            await _repositoryManager.SaveAsync();
            return true;
        }

        public async Task<List<BlogInList>> GetAllBlogs(int userId)
        {
            if (_userServices.IsAdmin(userId))
            {
                var adminBlog = await _repositoryManager.Post
                    .FindByCondition(x => x.IdType == (int)PostType.Blog && !x.IsDeleted, false)
                    .Select(x => new BlogInList
                    {
                        Id = x.Id,
                        CreateTime = x.SavedDate.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        ShortDescription = x.ContentPost.Substring(0, 100),
                        Title = x.Title,
                        Summary = x.AddressSlot,
                        ImgUrl = x.ImgUrl,

                    })
                    .ToListAsync();
                return adminBlog;
            }
            else
            if (await _userServices.IsStaff(userId))
            {
                var blogss = await _repositoryManager.Post
                    .FindByCondition(x => x.IdType == (int)PostType.Blog && !x.IsDeleted && x.IdUserTo == userId, false)
                    .Select(x => new BlogInList
                    {
                        Id = x.Id,
                        CreateTime = x.SavedDate.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        ShortDescription = x.ContentPost.Substring(0, 100),
                        Title = x.Title,
                        Summary = x.AddressSlot,
                        ImgUrl = x.ImgUrl,

                    })
                    .ToListAsync();
                return blogss;
            }

            var blogs = await _repositoryManager.Post
                .FindByCondition(x => x.IdType == (int)PostType.Blog && !x.IsDeleted, false)
                .Select(x => new BlogInList
                {
                    Id = x.Id,
                    CreateTime = x.SavedDate.ToString("dd/MM/yyyy hh:mm:ss tt"),
                    ShortDescription = x.ContentPost.Substring(0, 100),
                    Title = x.Title,
                    Summary = x.AddressSlot,
                    ImgUrl = x.ImgUrl,

                })
                .ToListAsync();

            return blogs;
        }

        public async Task<BlogDetail> GetBlogDetail(int blog_id)
        {
            var blog = await _repositoryManager.Post
                .FindByCondition(x => x.IdType == (int)PostType.Blog && x.Id == blog_id && !x.IsDeleted, false)
                .Select(x => new BlogDetail
                {
                    Id = x.Id,
                    CreateTime = x.SavedDate.ToString("dd/MM/yyyy HH:mm"),
                    Description = x.ContentPost,
                    Title = x.Title,
                    Summary = x.AddressSlot,
                    ImgUrl = x.ImgUrl
                }).FirstOrDefaultAsync();

            return blog;
        }

        public async Task<int> UpdatePost(UpdatetPost post)
        {

            var avaiableSlot = _repositoryManager.SlotPost.FindByCondition(x => x.IdPost == post.idPost, false).ToList();
            if (post != null || avaiableSlot != null && post.SlotInfor.TimeSlot.Count > 0)
            {
                if (DateTime.ParseExact(post.SlotInfor.TimeSlot.FirstOrDefault(), "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(avaiableSlot.LastOrDefault().SlotDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    return 2;
                }
                else
                {
                    try
                    {
                        foreach (String slotDate in post.SlotInfor.TimeSlot)
                        {

                            foreach (String timeSlot in timeSlots)
                            {
                                SlotPost slot = new SlotPost();
                                slot.IdPost = post.idPost;
                                slot.ContextPost = timeSlot;
                                slot.SlotDate = slotDate;
                                slot.SlotPrice = (decimal)post.SlotInfor.Price;
                                _repositoryManager.SlotPost.Create(slot);

                            }
                        }
                        await _repositoryManager.SaveAsync();
                        return 1;
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }

            return -1;
        }

        public async Task<List<SlotCheckStatus>> GetPostStatus(int id_post)
        {
            var post = await _repositoryManager.Post
                .FindByCondition(x =>
                    x.Id == id_post
                    && !x.IsDeleted
                    && x.IdType == (int)PostType.MatchingPost, true)
                .Include(x => x.SlotPosts)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return new List<SlotCheckStatus>();
            }

            post.TotalViewer++;
            await _repositoryManager.SaveAsync();

            var listSlot = await _repositoryManager.Slot
                .FindByCondition(x => x.IdPost == id_post, false)
                .Include(x => x.IdSlotNavigation)
                .ToListAsync();

            var postSlot = new List<SlotCheckStatus>();
            var today = DateTime.Now.Date;
            var dates = post.SlotPosts
                .Where(sp => sp.IdPost == post.Id && DateTime.ParseExact(sp.SlotDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= today)
                .Select(sp => sp.SlotDate)
                .Distinct()
                .ToList();

            foreach (var date in dates)
            {
                var ps = new SlotCheckStatus { DateSlot = date, slots = new List<slotCheck>() };


                foreach (var iNaviSlot in post.SlotPosts)
                {
                    var slotss = new slotCheck();
                    if (iNaviSlot.SlotDate == date)
                    {
                        if (iNaviSlot.Status != (int)SlotStatus.Watting)
                        {
                            var slots = await _repositoryManager.Slot.FindByCondition(x => x.IdSlot == iNaviSlot.IdSlot, false).FirstOrDefaultAsync();
                            if (slots != null)
                            {
                                slotss.transisionId = slots.TransactionId;
                            }


                        }

                        slotss.content = iNaviSlot.ContextPost;
                        slotss.status = iNaviSlot.Status;
                        ps.slots.Add(slotss);
                    }


                }
                postSlot.Add(ps);

            }
            return postSlot;
        }

        public async Task<List<PostOptional>> GetPostByPlayGround(string play_ground)
        {
            var listpost = await _repositoryManager.Post.FindByCondition(x => x.AddressSlot == play_ground & !x.IsDeleted && x.IdType == (int)PostType.MatchingPost, false).OrderByDescending(x => x.SavedDate).ToListAsync();
            var res = new List<PostOptional>();
            foreach (var post in listpost)
            {

                res.Add(new PostOptional
                {
                    IdPost = post.Id,
                    Title = post.Title,
                    AddressSlot = post.AddressSlot,
                    ContentPost = post.ContentPost,
                    ImgUrlPost = post.ImgUrl,
                    HighlightUrl = post.ImgUrl,
                    UserId = post.IdUserTo
                });

            }

            return res;
        }

    }
}
