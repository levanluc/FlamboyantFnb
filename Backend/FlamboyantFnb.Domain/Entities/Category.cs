using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlamboyantFnb.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int MerchantId { get; set; }
        [ForeignKey(nameof(MerchantId))]
        public Merchant Merchant { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
