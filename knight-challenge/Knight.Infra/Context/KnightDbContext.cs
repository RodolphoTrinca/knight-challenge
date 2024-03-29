using Knight.Application.Entity;
using Microsoft.EntityFrameworkCore;
using KnightEntity = Knight.Application.Entity.Knight;

namespace Knight.Infra.Context
{
    public class KnightDbContext : DbContext
    {
        public DbSet<KnightEntity> Knights { get; init; }

        public KnightDbContext(DbContextOptions<KnightDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KnightEntity>();
        }
    }
}
