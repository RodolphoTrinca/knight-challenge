using MongoDB.Bson;

namespace Knight.Application.Repository
{
    public interface IKnightRepository
    {
        Entity.Knight GetById(ObjectId id);
        IEnumerable<Entity.Knight> GetAll(int skip = 0, int take = 10);
        void Remove(Entity.Knight obj);
        void SaveOrUpdate(Entity.Knight obj);
    }
}