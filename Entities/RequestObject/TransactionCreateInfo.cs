using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.RequestObject
{
    public class TransactionCreateInfo
    {
        
     
        public int IdPost { get; set; }
        public int? IdUser { get; set; }
        public List<PostSlot> postSlot { get; set; }
       
    }
}
