namespace Entities.Models
{
    public enum ReportStatus
    {
        Pending = 0,
        Reviewing = 1,
        Resolved = 2
    }

    public enum ReportCreateType
    {
        Post = 0,
        Transaction = 1,
        User = 2
    }

    public partial class Report
    {
        public int Id { get; set; }
        public int? IdTransaction { get; set; }
        public int? IdUserFrom { get; set; }
        public int? IdUserTo { get; set; }
        public int? IdPost { get; set; }
        public int? IdRoom { get; set; }
        public DateTime? TimeReport { get; set; }
        public int? Status { get; set; }
        public string? reportContent { get; set; }
        public string? ReportTitle { get; set; }

        public virtual Transaction? Transaction { get; set; }
        public virtual Post? Post { get; set; }
    }
}
