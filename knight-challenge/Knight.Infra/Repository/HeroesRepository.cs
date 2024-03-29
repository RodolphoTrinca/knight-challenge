using Knight.Infra.Context;
using MongoDB.Bson;
using Entity = Knight.Application.Entity.Knight;

namespace Knight.Infra.Repository
{
    public class HeroesRepository
    {
        private readonly KnightDbContext _context;

        public HeroesRepository(KnightDbContext context)
        {
            _context = context;
        }

        public Entity GetById(ObjectId id)
        {
            return _context.Heroes.FirstOrDefault(k => k.Id == id);
        }

        public IEnumerable<Entity> GetAll(int skip = 0, int take = 10)
        {
            return _context.Heroes
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public void Remove(Entity obj)
        {
            _context.Heroes.Remove(obj);
            _context.SaveChanges();
        }

        public void SaveOrUpdate(Entity obj)
        {
            if (obj.Id == ObjectId.Empty)
            {
                _context.Heroes.Add(obj);
            }

            _context.SaveChanges();
        }
    }
}
