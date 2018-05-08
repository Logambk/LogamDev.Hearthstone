using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IUserInteractionProcessor
    {
        List<EventBase> ProcessInteraction(InternalState me, InternalState opp, InteractionBase interaction);
        ValidationResult ValidateUserInteraction(GameState state, InteractionBase interaction);
    }
}
