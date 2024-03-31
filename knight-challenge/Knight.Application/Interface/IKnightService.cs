using MongoDB.Bson;

namespace Knight.Application.Interface
{
    public interface IKnightService
    {
        Entity.Knight GetById(ObjectId id);
        IEnumerable<Entity.Knight> GetAll(string filter = "", int skip = 0, int take = 10);
        void Remove(Entity.Knight obj);
        void Save(Entity.Knight obj);
        IEnumerable<KnightHall> GetHallOfKnights(string filter, int skip = 0, int take = 100);
    }
}
