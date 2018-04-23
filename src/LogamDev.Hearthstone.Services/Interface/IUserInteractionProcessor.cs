using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IUserInteractionProcessor
    {
        List<GameEvent> ProcessInteraction(
            InternalSide internalSideActivePlayer,
            InternalSide opponentSide,
            UserInteractionBase userInteraction);
    }
}
