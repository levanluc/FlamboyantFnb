using System;
using System.Collections.Generic;
using System.Text;

namespace FlamboyantFnb.Domain.RequestModel
{
    public class LoginReq
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
