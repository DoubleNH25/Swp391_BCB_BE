using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class HangfireJob
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string? ScheduledId { get; set; }

        public Transaction? Transaction { get; set; }
    }
}
