using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IUserInteractionProcessor
    {
        List<EventBase> ProcessInteraction(ServerGameState fullState, InteractionBase interaction);
        ValidationResult ValidateInteraction(ClientGameState state, InteractionBase interaction);
    }
}
