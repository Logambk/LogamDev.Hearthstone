using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.State
{
    /// <summary>
    /// This is how you see yourself
    /// </summary>
    public class ClientPlayerState
    {
        public int DeckSize { get; set; }
        public Dictionary<int, List<EventBase>> Events { get; set; }
        public List<CardBase> Hand { get; set; }       
        public ManaStorage Mana { get; set; }
        public List<Minion> Minions { get; set; }
        public Player Player { get; set; }
    }
}
