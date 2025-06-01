using System;
using System.Collections.Generic;
using System.Text;

namespace FlamboyantFnb.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
