using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.State
{
    /// <summary>
    /// This is the main class that describes full state of a player (internally at server)
    /// </summary>
    public class ServerPlayerState
    {
        public List<CardBase> Deck { get; set; }
        public List<CardBase> Hand { get; set; }
        public ManaStorage Mana { get; set; }
        public List<Minion> Minions { get; set; }
        public Deck OriginalDeck { get; set; }
        public Player Player { get; set; }
        public TriggerStorage Triggers { get; set; }
        public int MinionOrderNumber { get; set; }
        public Dictionary<int, List<EventBase>> Events { get; set; }
    }
}
