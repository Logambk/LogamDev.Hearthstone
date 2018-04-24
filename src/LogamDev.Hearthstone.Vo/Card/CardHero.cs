using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Card
{
    public class CardHero : CardBase
    {
        public int Armor { get; set; }

        public CardHero() : base(CardType.Hero)
        {
        }

        public override CardBase Clone()
        {
            return new CardHero(this);
        }

        protected CardHero(CardHero other) : base(other)
        {
            Armor = other.Armor;
        }
    }
}
