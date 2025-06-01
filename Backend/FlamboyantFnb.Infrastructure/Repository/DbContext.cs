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
