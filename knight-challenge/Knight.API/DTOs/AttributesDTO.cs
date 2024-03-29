using Knight.Application.Entity;

namespace Knight.DTOs
{
    public class AttributesDTO
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constituition { get; set; }
        public int Inteligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public AttributesDTO(Attributes attributes)
        {
            Strength = attributes.Strength;
            Dexterity = attributes.Dexterity;
            Constituition = attributes.Constituition;
            Inteligence = attributes.Inteligence;
            Wisdom = attributes.Wisdom;
            Charisma = attributes.Charisma;
        }

        public AttributesDTO()
        {
            
        }

        public Attributes ToAttributes()
        {
            return new Attributes()
            {
                Strength = Strength,
                Dexterity = Dexterity,
                Constituition = Constituition,
                Inteligence = Inteligence,
                Wisdom = Wisdom,
                Charisma = Charisma,
            };
        }
    }
}
