using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Auth
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }    // "Username đã tồn tại", "Đăng ký thành công", ...
    }
}
