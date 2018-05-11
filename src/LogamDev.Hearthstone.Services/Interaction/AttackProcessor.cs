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

        public void ProcessAttack(ServerGameState fullState, InteractionAttack interactionAttack)
        {
            var newEvent = new EventCharacterAttacks() { Attacker = interactionAttack.Attacker, Attacked = interactionAttack.Target };
            eventProcessor.ProcessEvent(fullState, newEvent);
        }

        public ValidationResult ValidateAttack(ClientGameState currentState, InteractionAttack attackInteraction)
        {
            //TODO: implement validation
            return new ValidationResult() { IsOk = true };
        }
    }
}
