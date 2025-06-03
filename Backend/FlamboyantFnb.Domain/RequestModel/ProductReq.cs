using System;
using System.Collections.Generic;
using System.Text;

namespace FlamboyantFnb.Domain.RequestModel
{
    public class ProductFilterReq : BasePagingReq
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ProductReq
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
