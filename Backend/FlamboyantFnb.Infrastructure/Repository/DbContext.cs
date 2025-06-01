using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlamboyantFnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlamboyantFnb.Infrastructure.Repository
{
    public class FnbDbContext : DbContext
    {
        public FnbDbContext(DbContextOptions<FnbDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
