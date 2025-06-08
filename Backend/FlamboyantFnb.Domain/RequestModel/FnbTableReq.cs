using System.ComponentModel.DataAnnotations;

namespace FlamboyantFnb.Domain.RequestModel
{
    public class FnbTableFilterReq : BasePagingReq
    {
        public string Name { get; set; }
        public int? Status { get; set; }
    }
    public class FnbTableAddReq
    {
        [Required(ErrorMessage = "TableName is required")]
        [MaxLength(50, ErrorMessage = "TableName cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "TableSeatCount is required")]
        public int SeatCount { get; set; }

        [MaxLength(50, ErrorMessage = "TableName cannot exceed 50 characters")]
        public string Location { get; set; }
    }


    public class FnbTableUpdateReq
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "TableName is required")]
        [MaxLength(50, ErrorMessage = "TableName cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "TableSeatCount is required")]
        public int SeatCount { get; set; }

        [MaxLength(50, ErrorMessage = "Location cannot exceed 50 characters")]
        public string Location { get; set; }

    }
}
