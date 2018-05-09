using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.State
{
    public class InternalSide
    {
        public List<CardBase> Deck { get; set; }
        public List<CardBase> Hand { get; set; }
        public ManaStorage Mana { get; set; }
        public List<Minion> Minions { get; set; }
        public Player Player { get; set; }
        public TriggerStorage Triggers { get; set; }
        public int MinionOrderNumber { get; set; }
        public Dictionary<int, List<EventBase>> Events { get; set; }
    }
}
