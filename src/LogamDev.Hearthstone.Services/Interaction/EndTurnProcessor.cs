using System;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Interaction
{
    public class EndTurnProcessor
    {
        public void ProcessEndTurn(ServerGameState fullState, InteractionEndTurn interactionEndTurn)
        {
            throw new NotImplementedException();
        }

        public ValidationResult ValidateEndTurn(ClientGameState currentState)
        {
            //TODO: implement validation
            return new ValidationResult() { IsOk = true };
        }
    }
}
