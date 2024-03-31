using Knight.Application.Entity;
using Knight.Infra.Context;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Knight.Infra.Repository
{
    public class HeroRepository : IHeroRepository
    {
        private readonly KnightDbContext _context;
        private readonly ILogger<HeroRepository> _logger;

        public HeroRepository(KnightDbContext context, ILogger<HeroRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Hero GetById(ObjectId id)
        {
            return _context.Heroes.FirstOrDefault(k => k.Id == id);
        }

        public IEnumerable<Hero> GetAll(int skip = 0, int take = 10)
        {
            return _context.Heroes
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public void Remove(Hero obj)
        {
            _context.Heroes.Remove(obj);

            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);
            _context.SaveChanges();
        }

        public void Save(Hero obj)
        {
            if (obj.Id == ObjectId.Empty)
            {
                _context.Heroes.Add(obj);
            }

            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);
            _context.SaveChanges();
        }
    }
}
