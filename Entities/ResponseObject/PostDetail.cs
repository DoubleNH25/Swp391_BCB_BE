using Entities.Models;
using Entities.Models;
using Entities.RequestObject;

namespace Entities.ResponseObject
{
    public class PostDetail
    {
        public PostDetail()
        {

        }
        public PostDetail(Post post)
        {
            AddressSlot = post.AddressSlot;
            CategorySlot = post.CategorySlot;
            ContentPost = post.ContentPost;
          
            HightLightImage = post.ImgUrl;     
            UserId = post.IdUserTo.Value;
            Title = post.Title;
            var DateSlot=new DateTime() ;
            Price = post.SlotPosts.FirstOrDefault().SlotPrice;          
        }

        public string? AddressSlot { get; set; }
        public string? LevelSlot { get; set; }
        public string? CategorySlot { get; set; }
        public string? ContentPost { get; set; }
        public string? HightLightImage { get; set; }
        public List<string>? ImageUrls { get; set; }
        public string? FullName { get; set; }
        public int? TotalRate { get; set; }
        public string? ImgUrlUser { get; set; }
        public string? SortProfile { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public Decimal? Price { get; set; }
        public List<PostSlot> postSlot { get; set; }
        
    }
}
