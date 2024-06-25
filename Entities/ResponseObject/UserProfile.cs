using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class UserProfile
    {
        public string? FullName { get; set; }
        public int? TotalRate { get; set; }
        public string? ImgUrl { get; set; }
        public string? SortProfile { get; set; }
        public double? LevelSkill { get; set; }
        public double? Friendly { get; set; }
        public double? Trusted { get; set; }
        public double? Helpful { get; set; }
        public string PlayingArea { get; set; }
    }
}
