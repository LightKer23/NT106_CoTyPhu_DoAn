using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Auth
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public int AccountID { get; set; }    // -1 nếu fail
        public string DisplayName { get; set; }
        public string Message { get; set; }
    }
}
