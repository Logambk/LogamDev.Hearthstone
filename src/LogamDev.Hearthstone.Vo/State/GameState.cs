using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;

namespace LogamDev.Hearthstone.Vo.State
{
    public class GameState
    {
        public InternalState Me { get; set; }
        public ExternalState Opp { get; set; }

        //TODO: re-think about how to handle temporary turn things like attacks
        public List<EventBase> ThisTurnEvents { get; set; }
    }
}
