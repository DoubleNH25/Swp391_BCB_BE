using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestObject
{
    public class UpdatetPost
    {
        public int idPost { get; set; }
        public SlotInfo? SlotInfor { get; set; }
    }

}
