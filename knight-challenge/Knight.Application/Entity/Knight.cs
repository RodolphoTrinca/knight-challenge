using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Knight.Application.Entity
{
    public class Knight
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public DateTime BirthDay { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Attributes Attributes { get; set; }
        public string KeyAttribute { get; set; }

        public Knight(Hero hero)
        {
            Id = hero.Id;
            Name = hero.Name;
            Nickname = hero.Nickname;
            BirthDay = hero.BirthDay;
            Weapons = hero.Weapons;
            Attributes = hero.Attributes;   
            KeyAttribute = hero.KeyAttribute;
        }

        public Knight()
        {
            
        }

        internal Knight Update(Knight obj)
        {
            if (obj == null)
            {
                return this;
            }

            Id = obj.Id;
            Name = obj.Name;
            Nickname = obj.Nickname;
            BirthDay = obj.BirthDay;
            Weapons = obj.Weapons;
            Attributes = obj.Attributes;
            KeyAttribute = obj.KeyAttribute;

            return this;
        }
    }
}
