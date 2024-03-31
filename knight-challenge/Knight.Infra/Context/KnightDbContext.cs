using Knight.Application.Entity;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using KnightEntity = Knight.Application.Entity.Knight;

namespace Knight.Infra.Context
{
    public class KnightDbContext : DbContext
    {
        public DbSet<KnightEntity> Knights { get; init; }
        public DbSet<Hero> Heroes { get; init; }

        public KnightDbContext(DbContextOptions<KnightDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KnightEntity>().ToCollection("knights");
            modelBuilder.Entity<Hero>().ToCollection("heroes");
        }
    }
}
