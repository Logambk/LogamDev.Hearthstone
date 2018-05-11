using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Interaction
{
    public class AttackProcessor
    {
        private readonly IEventProcessor eventProcessor;

        public AttackProcessor(IEventProcessor eventProcessor)
        {
            this.eventProcessor = eventProcessor;
        }

        public List<EventBase> ProcessAttack(ServerGameState fullState, InteractionAttack interactionAttack)
        {
            var newEvent = new EventCharacterAttacks() { Attacker = interactionAttack.Attacker, Attacked = interactionAttack.Target };
            var actualEvents = eventProcessor.ProcessEvent(fullState, newEvent);
            return actualEvents;
        }

        public ValidationResult ValidateAttack(ClientGameState currentState, InteractionAttack attackInteraction)
        {
            //TODO: implement validation
            return new ValidationResult() { IsOk = true };
        }
    }
}
