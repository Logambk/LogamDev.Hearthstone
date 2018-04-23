using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class InternalSide
    {
        public Player Player { get; set; }
        public List<Minion> Minions { get; set; }
        public List<CardBase> Hand { get; set; }
        public List<CardBase> Deck { get; set; }
    }
}
