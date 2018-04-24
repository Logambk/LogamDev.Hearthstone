using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Card
{
    public class CardSpell : CardBase
    {
        public CardSpell() : base(CardType.Spell)
        {
        }

        public override CardBase Clone()
        {
            return new CardSpell(this);
        }

        protected CardSpell(CardSpell other) : base(other)
        {
        }
    }
}
