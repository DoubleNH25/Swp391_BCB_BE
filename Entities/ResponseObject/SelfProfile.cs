using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class SelfProfile
    {
        public string UserName { get; set; } = null!;
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PlayingArea { get; set; }
        public string? SortProfile { get; set; }
        public string? ImgUrl { get; set; }
    }
}
