using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class Reports
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? DateReceive { get; set; }
        public string? Status { get; set; }
        public int? NavigationId { get; set; }
        public string? ObjectNavigation { get; set; }
    }
}
