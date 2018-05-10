using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.State
{
    /// <summary>
    /// This is how you see the opponent
    /// </summary>
    public class ClientOpponentState
    {
        public int DeckSize { get; set; }
        public int HandSize { get; set; }
        public ManaStorage Mana { get; set; }
        public List<Minion> Minions { get; set; }
        public Player Player { get; set; }
        public Dictionary<int, List<EventBase>> VisibleEvents { get; set; }
    }
}
