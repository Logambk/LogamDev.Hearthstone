using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.GameEvent;

namespace LogamDev.Hearthstone.Vo.State
{
    public class GameState
    {
        public InternalState Me { get; set; }
        public ExternalState Opp { get; set; }

        //TODO: re-think about how to handle temporary turn things like attacks
        public List<GameEventBase> ThisTurnEvents { get; set; }
    }
}
