using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IGameStatePreparator
    {
        GameState PrepareGameState(InternalState me, InternalState opp, List<EventBase> thisTurnEvents);
        InternalState Initialize(PlayerInitializer playerInitializer);
    }
}
