using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseObject
{
    public class VerifyEmail
    {
        public string? Otp { get; set; }
        public string? Token { get; set; }
    }
}
