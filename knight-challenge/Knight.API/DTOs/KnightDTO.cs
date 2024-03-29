using MongoDB.Bson;
using Entity = Knight.Application.Entity.Knight;

namespace Knight.DTOs
{
    public class KnightDTO
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string BirthDay { get; set; }
        public List<WeaponDTO> Weapons { get; set; }
        public AttributesDTO Attributes { get; set; }
        public string KeyAttribute { get; set; }

        public KnightDTO(Entity knight)
        {
            Id = knight.Id;
            Name = knight.Name;
            Nickname = knight.Nickname;
            BirthDay = knight.BirthDay;
            Weapons = knight.Weapons?.Select(w => new WeaponDTO(w)).ToList();
            Attributes = knight.Attributes != null ? new AttributesDTO(knight.Attributes) : null;
            KeyAttribute = knight.KeyAttribute;
        }

        public KnightDTO() { }

        public Entity ToKnight()
        {
            return new Entity()
            {
                Id = Id,
                Attributes = Attributes.ToAttributes(),
                BirthDay  = BirthDay,
                KeyAttribute = KeyAttribute,
                Name = Name,
                Nickname = Nickname,
                Weapons = Weapons?.Select(w => w.ToWeapon()).ToList()
            };
        }
    }
}
