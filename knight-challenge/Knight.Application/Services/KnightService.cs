using Knight.Application.Interface;
using Knight.Application.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Knight.Application.Services
{
    public class KnightService : IKnightService
    {
        private readonly IKnightRepository _repository;

        public KnightService(IKnightRepository repository)
        {
            _repository = repository;
        }

        public Entity.Knight GetById(ObjectId id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Entity.Knight> GetAll(int skip = 0, int take = 10)
        {
            return _repository.GetAll(skip, take);
        }

        public void Remove(Entity.Knight obj)
        {
            _repository.Remove(obj);
        }

        public void Save(Entity.Knight obj)
        {
            var changedIncome = obj;
            if (obj.Id != ObjectId.Empty)
            {
                var oldObject = GetById(obj.Id);
                changedIncome = oldObject.Update(obj);
            }

            _repository.SaveOrUpdate(changedIncome);
        }
    }
}
