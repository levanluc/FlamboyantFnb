using FlamboyantFnb.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Text;

namespace FlamboyantFnb.Domain.RequestModel
{
    public class CategoryAddReq
    {
        [Required(ErrorMessage = "CategoryName is required")]
        [MaxLength(50, ErrorMessage = "CategoryName cannot exceed 50 characters")]
        public string Name { get; set; }
    }
}
