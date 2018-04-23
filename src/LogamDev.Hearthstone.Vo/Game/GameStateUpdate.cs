using System.Collections.Generic;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class GameStateUpdate
    {
        public List<GameEvent> Events { get; set; }
        public GameState NewState { get; set; }
    }
}
