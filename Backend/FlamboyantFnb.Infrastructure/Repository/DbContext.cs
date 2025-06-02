using FlamboyantFnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlamboyantFnb.Infrastructure.Repository
{
    public class FnbDbContext : DbContext
    {
        public FnbDbContext(DbContextOptions<FnbDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Merchant> Merchants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
