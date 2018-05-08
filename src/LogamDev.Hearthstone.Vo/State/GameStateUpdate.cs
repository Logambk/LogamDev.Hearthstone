using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;

namespace LogamDev.Hearthstone.Vo.State
{
    public class GameStateUpdate
    {
        public List<EventBase> Events { get; set; }
        public GameState NewState { get; set; }
    }
}
