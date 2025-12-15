using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }     // có thể hash ở client hoặc để plain rồi hash ở server
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
