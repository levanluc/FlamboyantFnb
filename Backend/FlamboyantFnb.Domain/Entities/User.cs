using FlamboyantFnb.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlamboyantFnb.Domain.Entities
{
    public class User : BaseEntity, IMerchantId
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int MerchantId { get; set; }
        [ForeignKey(nameof(MerchantId))]
        public Merchant Merchant { get; set; }
    }
}
