using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.State
{
    public class ExternalSide
    {
        public Player Player { get; set; }
        public ManaStorage ManaStorage { get; set; }
        public List<Minion> Minions { get; set; }
        public int DeckSize { get; set; }
        public int HandSize { get; set; }
        public Dictionary<int, List<EventBase>> VisibleEvents { get; set; }
    }
}
