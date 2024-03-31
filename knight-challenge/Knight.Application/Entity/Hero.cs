using MongoDB.Bson;

namespace Knight.Application.Entity
{

    public class Hero
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public DateTime BirthDay { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Attributes Attributes { get; set; }
        public string KeyAttribute { get; set; }
        
        public Hero(Knight knight)
        {
            Id = knight.Id;
            Name = knight.Name;
            Nickname = knight.Nickname;
            BirthDay = knight.BirthDay;
            Weapons = knight.Weapons;
            Attributes = knight.Attributes;
            KeyAttribute = knight.KeyAttribute;
        }

        public Hero()
        {
            
        }

        internal Hero Update(Hero obj)
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