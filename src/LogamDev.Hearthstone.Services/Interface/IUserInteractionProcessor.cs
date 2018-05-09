using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IUserInteractionProcessor
    {
        List<EventBase> ProcessInteraction(FullGameState fullState, InteractionBase interaction);
        ValidationResult ValidateInteraction(GameState state, InteractionBase interaction);
    }
}
