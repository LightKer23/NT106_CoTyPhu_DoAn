using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }     // hash trước khi gửi
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
