using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Card
{
    public abstract class CardBase
    {
        public CardClass Class { get; set; }
        public int Cost { get; set; }
        public int DbfId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CardRarity Rarity { get; set; }
        public CardType Type { get; private set; }

        public abstract CardBase Clone();

        public CardBase(CardType type)
        {
            Type = type;
        }

        protected CardBase(CardBase other)
        {
            Class = other.Class;
            Cost = other.Cost;
            DbfId = other.DbfId;
            Id = other.Id;
            Name = other.Name;
            Rarity = other.Rarity;
            Type = other.Type;
        }
    }
}
