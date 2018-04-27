using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IGameStatePreparator
    {
        GameState PrepareGameState(InternalState me, InternalState opp, List<GameEventBase> thisTurnEvents);
        InternalState Initialize(PlayerInitializer playerInitializer);
    }
}
