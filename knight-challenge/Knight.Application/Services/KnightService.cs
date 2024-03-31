using Knight.Application.Entity;
using Knight.Application.Interface;
using Knight.Application.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Knight.Application.Services
{
    public class KnightService : IKnightService
    {
        private readonly IKnightRepository _knightRepository;
        private readonly IHeroRepository _heroRepository;

        public KnightService(IKnightRepository knightRepository, IHeroRepository heroRepository)
        {
            _knightRepository = knightRepository;
            _heroRepository = heroRepository;
        }

        public Entity.Knight GetById(ObjectId id)
        {
            return _knightRepository.GetById(id);
        }

        public IEnumerable<Entity.Knight> GetAll(string filter = "", int skip = 0, int take = 10)
        {
            if (filter.Equals("heroes"))
            {
                var heroes = _heroRepository.GetAll(skip, take);
                return heroes.Select(h => new Entity.Knight(h)).ToList();
            }

            return _knightRepository.GetAll(skip, take);
        }

        public void Remove(Entity.Knight obj)
        {
            _knightRepository.Remove(obj);

            var hero = new Entity.Hero(obj);
            hero.Id = ObjectId.Empty;
            _heroRepository.Save(hero);
        }

        public void Save(Entity.Knight obj)
        {
            var changedIncome = obj;
            if (obj.Id != ObjectId.Empty)
            {
                var oldObject = GetById(obj.Id);
                changedIncome = oldObject.Update(obj);
            }

            _knightRepository.SaveOrUpdate(changedIncome);
        }

        public IEnumerable<KnightHall> GetHallOfKnights(string filter = "", int skip = 0, int take = 100)
        {
            var knights = GetAll(filter:filter, skip: skip, take: take);        

            return knights.Select(k =>
            {
                var age = GetAge(k.BirthDay);
                return new KnightHall()
                {
                    Name = k.Name,
                    Age = age,
                    Weapons = k.Weapons.Count,
                    Attribute = k.KeyAttribute,
                    Attack = GetAttack(k),
                    Experience = GetExperience(age)
                };
            }
            ).ToList();
        }

        private int GetExperience(int age)
        {
            if (age < 7)
            {
                return 0;
            }

            return (int)Math.Floor((age - 7) * Math.Pow(22, 1.45f));
        }

        private int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        private int GetAttack(Entity.Knight knight)
        {
            var attack = 10;

            var attributeValue = GetKeyAttributeValue(knight.KeyAttribute, knight.Attributes);
            var attributeMod = GetModifier(attributeValue);
            var equipedWeapon = knight.Weapons.FirstOrDefault(w => w.Equipped);

            attack += attributeMod + (equipedWeapon == null ? 0 : equipedWeapon.Mod);

            return attack;
        }

        private int GetKeyAttributeValue(string attribute, Attributes attributes)
        {
            switch (attribute)
            {
                case "strength": return attributes.Strength;
                case "dexterity": return attributes.Dexterity;
                case "constitution": return attributes.Constituition;
                case "intelligence": return attributes.Inteligence;
                case "wisdom": return attributes.Wisdom;
                case "charisma": return attributes.Charisma;
                default: return 0;
            }
        }

        private int GetModifier(int defaultValue)
        {
            switch (defaultValue)
            {
                case var _ when defaultValue <= 8:
                    return -2;
                case var _ when defaultValue <= 10:
                    return -1;
                case var _ when defaultValue <= 12:
                    return 0;
                case var _ when defaultValue <= 15:
                    return 1;
                case var _ when defaultValue <= 18:
                    return 2;
                case var _ when defaultValue <= 20:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
