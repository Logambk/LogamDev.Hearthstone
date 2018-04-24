using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IUserInteractionProcessor
    {
        List<GameEventBase> ProcessInteraction(
            InternalSide internalSideActivePlayer,
            InternalSide opponentSide,
            InteractionBase userInteraction);

        ValidationResult ValidateUserInteraction(GameState currentState, InteractionBase interaction);
    }
}
