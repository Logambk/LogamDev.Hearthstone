using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.State
{
    public class PlayerInitializer
    {
        public string Name { get; set; }
        public List<CardBase> Deck { get; set; }
        public CardClass Class { get; set; }
    }
}
