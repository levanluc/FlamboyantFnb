using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlamboyantFnb.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public bool IsMaster { get; set; }
        public byte Type { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Price { get; set; }
        public decimal OnHand { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public int MerchantId { get; set; }
        [ForeignKey(nameof(MerchantId))]
        public Merchant Merchant { get; set; }
    }
}
