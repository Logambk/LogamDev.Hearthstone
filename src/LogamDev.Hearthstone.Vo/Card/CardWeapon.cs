using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Card
{
    public class CardWeapon : CardBase
    {
        public int Attack { get; set; }
        public int Durability { get; set; }

        public CardWeapon() : base(CardType.Weapon)
        {
        }

        public override CardBase Clone()
        {
            return new CardWeapon(this);
        }

        protected CardWeapon(CardWeapon other) : base(other)
        {
            Attack = other.Attack;
            Durability = other.Durability;
        }
    }
}
