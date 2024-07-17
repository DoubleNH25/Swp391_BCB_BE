using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class ListPostByAdmin
    {
        public int? IdUser { get; set; }
        public string? FullName {  get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? RoleUser {  get; set; }
        public bool? Status { get; set; }
        public string? TotalViewer { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
