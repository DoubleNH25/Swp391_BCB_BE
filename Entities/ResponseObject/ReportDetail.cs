using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.ResponseObject
{
    public class ReportDetail
    {
        public int? ReportId { get; set; }    
        public int? UserReportId { get; set; }
        public int? UserSendId { get; set; }
        public string? SendUserName { get; set; }
        public int? ReportStatus {  get; set; }
        public string? reportUserName { get; set; }
        public int? ReportType { get; set; }
        public string? TitleReport { get; set; }
        public string? ContentReport { get; set; }
        public bool ? IsBan { get; set; }

      
        
        public ReportPost reportPost { get; set; }=new ReportPost();
        public ReportTrans reportTrans { get; set; } = new ReportTrans();
    }

    public class ReportPost
    {
        public int? PostId { get; set; }
        public string? PostName { get; set; }
        public string? PostContent { get; set; }
        public string? PostDate { get; set; }
        public string? PostAddress { get; set; }
        public string? PostImage { get; set; }

    }

    public class ReportTrans
    {
        public int TransId { get; set; }
        public string? TransDate { get; set; }
        public Decimal? TransMoney { get; set; }

    }
}
