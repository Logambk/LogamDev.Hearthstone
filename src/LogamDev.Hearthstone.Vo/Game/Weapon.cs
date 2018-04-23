using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Weapon
    {
        public CardWeapon Card { get; }

        public int Attack { get; set; }
        public int Durability { get; set; }
    }
}
