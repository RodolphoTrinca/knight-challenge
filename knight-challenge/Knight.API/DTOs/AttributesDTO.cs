using System.ComponentModel.DataAnnotations;
using Knight.Application.Entity;

namespace Knight.DTOs
{
    public class AttributesDTO
    {
        
        [Range(0, 20, ErrorMessage = "The strength value has to be between 0 to 20")]
        public int Strength { get; set; }
        
        [Range(0, 20, ErrorMessage = "The dexterity value has to be between 0 to 20")]
        public int Dexterity { get; set; }
        
        [Range(0, 20, ErrorMessage = "The constituition value has to be between 0 to 20")]
        public int Constituition { get; set; }
        
        [Range(0, 20, ErrorMessage = "The inteligence value has to be between 0 to 20")]
        public int Inteligence { get; set; }
        
        [Range(0, 20, ErrorMessage = "The wisdom value has to be between 0 to 20")]
        public int Wisdom { get; set; }
        
        [Range(0, 20, ErrorMessage = "The charisma value has to be between 0 to 20")]
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
