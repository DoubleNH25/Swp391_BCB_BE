namespace Entities.RequestObject
{
    public class NewBlogInfo
    {
        public string? Title { get; set; }
        public string? Summary {  get; set; }
        public string? Description { get; set; }
        public List<string>? ImgUrls { get; set; }
        public string? HighlightImg {  get; set; }

    }
}
