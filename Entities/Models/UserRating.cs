namespace Entities.Models
{
    public partial class UserRating
    {
        public int Id { get; set; }
        public int? IdUserRate { get; set; }
        public int? IdUserRated { get; set; }
        public DateTime? Time { get; set; }
        public double? LevelSkill { get; set; }
        public double? Friendly { get; set; }
        public double? Trusted { get; set; }
        public double? Helpful { get; set; }
        public string? Content { get; set; }
        public int? IdTransaction { get; set; }

        public virtual HistoryTransaction? IdTransactionNavigation { get; set; }
        public virtual User? IdUserRatedNavigation { get; set; }
    }
}
