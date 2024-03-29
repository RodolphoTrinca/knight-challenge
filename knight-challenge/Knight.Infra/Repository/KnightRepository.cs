using Knight.Application.Repository;
using Knight.Infra.Context;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Entity = Knight.Application.Entity.Knight;

namespace Knight.Infra.Repository
{
    public class KnightRepository : IKnightRepository
    {
        private readonly KnightDbContext _context;
        private readonly ILogger<KnightRepository> _logger;

        public KnightRepository(KnightDbContext context, ILogger<KnightRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Entity GetById(ObjectId id)
        {
            return _context.Knights.FirstOrDefault(k => k.Id == id);
        }

        public IEnumerable<Entity> GetAll(int skip = 0, int take = 10)
        {
            return _context.Knights
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public void Remove(Entity obj)
        {
            _context.Knights.Remove(obj);

            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);
            
            _context.SaveChanges();
        }

        public void SaveOrUpdate(Entity obj)
        {
            if (obj.Id == ObjectId.Empty)
            {
                _context.Knights.Add(obj);
            }

            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);
            _context.SaveChanges();
        }
    }
}
