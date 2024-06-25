namespace Entities.Models
{
    public class VerifyToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }

        public virtual User? User { get; set; }
    }
}
