using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Auth
{
    public class VerifyOTPRequest
    {
        public string Email { get; set; } = "";
        public string OTP { get; set; } = "";
    }
}
