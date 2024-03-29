using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Knight.Application.Entity
{
    [Collection("knights")]
    public class Knight
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string BirthDay { get; set; }
        public List<Weapon> Weapons { get; set; }
        public Attributes Attributes { get; set; }
        public string KeyAttribute { get; set; }
    }
}
