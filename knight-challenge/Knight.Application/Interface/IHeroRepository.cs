using Knight.Application.Entity;
using MongoDB.Bson;

public interface IHeroRepository{
    Hero GetById(ObjectId id);
    IEnumerable<Hero> GetAll(int skip = 0, int take = 10);
    void Save(Hero obj);
    void Remove(Hero obj);
}