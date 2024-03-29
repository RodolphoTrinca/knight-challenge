using Knight.Application.Repository;
using Knight.Infra.Context;
using MongoDB.Bson;
using Entity = Knight.Application.Entity.Knight;

namespace Knight.Infra.Repository
{
    public class KnightRepository : IKnightRepository
    {
        private readonly KnightDbContext _context;

        public KnightRepository(KnightDbContext context)
        {
            _context = context;
        }

        public Entity GetById(ObjectId id)
        {
            return _context.Knights.Where(k => k.Id == id);
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
            _context.SaveChanges();
        }

        public void SaveOrUpdate(Entity obj)
        {
            if (obj.Id == null)
            {
                _context.Add(obj);
            }

            _context.SaveChanges();
        }
    }
}
