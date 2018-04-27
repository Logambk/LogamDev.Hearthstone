using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Player
    {
        public CardHero Card { get; }

        public int Armor { get; set; }
        public Weapon EquipedWeapon { get; set; }
        public int Health { get; set; }
        public string Name { get; set; }
        public CardClass Class { get; set; }
    }
}
