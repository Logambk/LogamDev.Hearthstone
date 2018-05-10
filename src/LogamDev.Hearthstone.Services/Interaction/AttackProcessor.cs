using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Interaction
{
    public class AttackProcessor
    {
        private readonly ILogger logger;
        public AttackProcessor(ILogger logger)
        {
            this.logger = logger;
        }

        public List<EventBase> ProcessAttack(ServerGameState fullState, InteractionAttack interactionAttack)
        {
            var me = fullState.Me;
            var opp = fullState.Opp;
            var events = new List<EventBase>();

            if (interactionAttack.Attacker != me.Player.Id && interactionAttack.Target == opp.Player.Id)
            {
                events.Add(new EventCharacterAttacks() { Attacker = interactionAttack.Attacker, Attacked = interactionAttack.Target });
                var attackingMinion = me.Minions.First(x => x.Id == interactionAttack.Attacker);

                logger.Log(LogType.Services, LogSeverity.Info, $"{attackingMinion.Card.Name} attacks {opp.Player.Name} for {attackingMinion.Attack} hp");

                opp.Player.Health -= attackingMinion.Attack;

                logger.Log(LogType.Services, LogSeverity.Info, $"{opp.Player.Name} Health reduced to {opp.Player.Health} hp");

                if (opp.Player.Health <= 0)
                {
                    events.Add(new EventPlayerDeath());
                }
            }

            //TODO: handle other types of attack targets
            return events;
        }

        public ValidationResult ValidateAttack(ClientGameState currentState, InteractionAttack attackInteraction)
        {
            //TODO: implement validation
            return new ValidationResult() { IsOk = true };
        }
    }
}
