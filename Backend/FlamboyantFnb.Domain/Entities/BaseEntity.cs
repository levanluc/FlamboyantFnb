using FlamboyantFnb.Domain.Interfaces;
using System;

namespace FlamboyantFnb.Domain.Entities
{
    public abstract class BaseEntity : ICreatedDate, IModifiedDate
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
