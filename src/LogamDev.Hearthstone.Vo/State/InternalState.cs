using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.State
{
    public class InternalState
    {
        public Player Player { get; set; }
        public ManaStorage Mana { get; set; }
        public List<Minion> Minions { get; set; }
        public List<CardBase> Hand { get; set; }
        public List<CardBase> Deck { get; set; }
    }
}
