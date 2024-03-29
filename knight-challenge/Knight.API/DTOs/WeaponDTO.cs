using Knight.Application.Entity;

namespace Knight.DTOs
{
    public class WeaponDTO
    {
        public string Name { get; set; }
        public int Mod { get; set; }
        public string Attr { get; set; }
        public bool Equipped { get; set; }

        public WeaponDTO(Weapon weapon)
        {
            Name = weapon.Name;
            Mod = weapon.Mod;
            Attr = weapon.Attr;
            Equipped = weapon.Equipped;
        }

        public WeaponDTO() { }

        public Weapon ToWeapon()
        {
            return new Weapon()
            {
                Name = Name,
                Mod = Mod,
                Attr = Attr,
                Equipped = Equipped
            };
        }
    }
}
