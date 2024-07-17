namespace Entities.RequestObject
{
    public class RatingUserInfo
    {
        public int? IdUserRate { get; set; }
        public int? IdUserRated { get; set; }
        public double? LevelSkill { get; set; }
        public double? Friendly { get; set; }
        public double? Trusted { get; set; }
        public double? Helpful { get; set; }
        public string? Content { get; set; }
        public int? IdTransaction { get; set; }
    }
}
