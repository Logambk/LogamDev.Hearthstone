using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Player
    {
        public CardHero Card { get; }

        public int Armor { get; set; }
        public Weapon EquipedWeapon { get; set; }
        public int Health { get; set; }
        public int TotalPermanentManaCrystals { get; set; }
        public int UsedPermanentManaCrystals { get; set; }
        public int TemporaryManaCrystals { get; set; }
    }
}
