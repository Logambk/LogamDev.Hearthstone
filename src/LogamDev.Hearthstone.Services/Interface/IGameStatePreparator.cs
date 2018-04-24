using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IGameStatePreparator
    {
        GameState PrepareGameState(InternalSide activeUser, InternalSide opponent, List<GameEventBase> thisTurnEvents);
        InternalSide Initialize(PlayerInitializer playerInitializer);
    }
}
