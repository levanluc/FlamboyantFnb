using FlamboyantFnb.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlamboyantFnb.Domain.Entities
{
    public class FnbTable : BaseEntity, IMerchantId
    {
        public string Name { get; set; }
        public int SeatCount { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }

        public int MerchantId { get; set; }
        [ForeignKey(nameof(MerchantId))]
        public Merchant Merchant { get; set; }
    }
}
