using System;
using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interaction
{
    public class EndTurnProcessor
    {
        public List<EventBase> ProcessEndTurn(FullGameState fullState, InteractionEndTurn interactionEndTurn)
        {
            throw new NotImplementedException();
        }

        public ValidationResult ValidateEndTurn(GameState currentState)
        {
            //TODO: implement validation
            return new ValidationResult() { IsOk = true };
        }
    }
}
