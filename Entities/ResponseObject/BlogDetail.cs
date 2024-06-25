using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class BlogDetail
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CreateTime { get; set; }
        public string? Description { get; set; }
        public string? UserCreateName { get; set; }
        public bool CanDelete { get; set; }
        public string? Summary { get; set; }
        public string? ImgUrl { get; set; }
    }
}
