using System;
using System.Collections.Generic;
using System.Text;

namespace FlamboyantFnb.Domain.RequestModel
{
    public class BasePagingReq
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
