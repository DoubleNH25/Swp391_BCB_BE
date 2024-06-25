using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class CreateChargerResponse
    {
        public int isUser { get; set; }
        public int idPost { get; set; }
        public decimal postingFree { get; set; }

    }
}
