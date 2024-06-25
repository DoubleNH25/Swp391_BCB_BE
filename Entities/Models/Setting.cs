using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum SettingType
    {
        PostingSetting = 1,
        BookingSetting = 2,
        NumberPostFree = 3,
        BoostPost = 4,
        CancelDate = 5
    }
    public class Setting
    {
        public int SettingId { get; set; }
        public string? SettingName { get; set; }
        public decimal? SettingAmount { get; set; }
    }
}//newcode for setting
