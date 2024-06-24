namespace Entities.Models
{
    public partial class TypePost
    {
        public TypePost()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string? TypePost1 { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}