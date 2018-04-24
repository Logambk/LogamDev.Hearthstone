using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Card
{
    public class CardMinion : CardBase
    {
        public int Attack { get; set; }
        public int Health { get; set; }

        public CardMinion() : base(CardType.Minion)
        {
        }

        public override CardBase Clone()
        {
            return new CardMinion(this);
        }

        protected CardMinion(CardMinion other) : base(other)
        {
            Attack = other.Attack;
            Health = other.Health;
        }
    }
}
