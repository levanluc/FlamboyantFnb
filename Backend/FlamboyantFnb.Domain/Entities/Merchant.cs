using System;
using System.Collections.Generic;
using System.Text;

namespace FlamboyantFnb.Domain.Entities
{
    public class Merchant : BaseEntity
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
