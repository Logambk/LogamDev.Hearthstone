using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Card
{
    public abstract class CardBase
    {
        public CardClass Class { get; set; }
        public int Cost { get; set; }
        public Guid Id { get; set; }
        public CardRarity Rarity { get; set; }
        public CardType Type { get; set; }
        public int DbfId { get; set; }
    }
}
