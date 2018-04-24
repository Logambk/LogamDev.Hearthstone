using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.GameEvent;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class GameStateUpdate
    {
        public List<GameEventBase> Events { get; set; }
        public GameState NewState { get; set; }
    }
}
