using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;

namespace Services.Interfaces
{
    public interface IPostServices
    {
        Task<bool> CreateBlog(int user_id, NewBlogInfo info);
        Task<int> CreatePost(int user_id, NewPostInfo info);
        Task<bool> DeletePostAsync(int post_id);
        Task<bool> DeleteBlogAsync(int post_id);
        Task<List<BlogInList>> GetAllBlogs(int userId);
        Task<List<PostOptional>> GetAllPost();
        Task<BlogDetail> GetBlogDetail(int blog_id);
        Task<List<JoinedPost>> GetJoined(int user_id);
        List<PostOptional> GetListOptionalPost();
        List<ListPostByAdmin> GetListPostByAdmin();
        List<PostInfomation> GetManagedPost(int user_id);
        List<PostInfomation> GetManagedPostAdmin(int user_id);
        Task<PostDetail> GetPostDetail(int id_post);
        Task<List<SlotCheckStatus>> GetPostStatus(int id_post);
        Task<string> HandleImg(string imgUrl);
        Task<int> UpdatePost(UpdatetPost post);
        Task<List<PostOptional>> GetPostByPlayGround(string play_ground);
    }
}
