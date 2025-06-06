using FlamboyantFnb.Domain.Context;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using FlamboyantFnb.Domain.RequestModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(FnbDbContext context, FnbExecutionContext executionContext) : base(context, executionContext)
        {
        }
    }
}
