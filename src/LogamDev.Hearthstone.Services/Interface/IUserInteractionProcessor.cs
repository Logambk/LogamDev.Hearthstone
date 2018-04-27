using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.GameEvent;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IUserInteractionProcessor
    {
        List<GameEventBase> ProcessInteraction(InternalState me, InternalState opp, InteractionBase interaction);

        ValidationResult ValidateUserInteraction(GameState state, InteractionBase interaction);
    }
}
