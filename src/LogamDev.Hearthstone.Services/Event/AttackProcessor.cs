using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Event
{
    public class AttackProcessor
    {
        private readonly ILogger logger;
        private readonly IEventProcessor eventProcessor;

        public AttackProcessor(ILogger logger, IEventProcessor eventProcessor)
        {
            this.logger = logger;
            this.eventProcessor = eventProcessor;
        }

        public void ProcessAttack(ServerGameState state, EventCharacterAttacks eventAttack)
        {
            // TODO: Process other types of attack (only Minion -> Player attacks are supported the moment)
            if (!IsAttackStillValid(state, eventAttack))
            {
                return;
            }

            // 1. Attack
            state.Me.LastTurnEvents.Add(eventAttack);

            // 2. Got Attacked
            var characterGotAttackedEvent = new EventCharacterGotAttacked() { Attacked = eventAttack.Attacked, Attacker = eventAttack.Attacker };
            eventProcessor.ProcessEvent(state, characterGotAttackedEvent);

            if (!IsAttackStillValid(state, eventAttack))
            {
                return;
            }

            // 3. Deal Damage
            var characterDealsDamageEvent = new EventCharacterDealsDamage() { Damaged = eventAttack.Attacked, Damager = eventAttack.Attacker };
            eventProcessor.ProcessEvent(state, characterDealsDamageEvent);

            if (!IsAttackStillValid(state, eventAttack))
            {
                return;
            }

            //TODO: process it inside EventCharacterDealsDamage
            var attackingMinion = state.Me.Minions.FirstOrDefault(x => x.Id == eventAttack.Attacker);
            logger.Log(LogType.Services, LogSeverity.Info, $"{attackingMinion.Card.Name} attacks {state.Me.Player.Name} for {attackingMinion.Attack} damage");

            // 4 Got Damaged
            //TODO: check if the damage event actually happened and was not somehow nullified
            var characterDamagedEvent = new EventCharacterDamaged() { Damaged = eventAttack.Attacked, Damager = eventAttack.Attacker };
            eventProcessor.ProcessEvent(state, characterDamagedEvent);

            //TODO: process it inside EventCharacterDamaged
            state.Opp.Player.Health -= attackingMinion.Attack;
            logger.Log(LogType.Services, LogSeverity.Info, $"{state.Opp.Player.Name} Health reduced to {state.Opp.Player.Health} hp");

            // 5. Check if Opp died
            if (state.Opp.Player.Health <= 0)
            {
                var charactedDiedEvent = new EventCharacterDied() { DiedCharacter = eventAttack.Attacked };
                eventProcessor.ProcessEvent(state, charactedDiedEvent);
            }
            else
            {
                var characterSurvivedDamage = new EventCharacterSurvivedDamage();
                eventProcessor.ProcessEvent(state, characterSurvivedDamage);
            }

            // 6. After Attack Event
            var afterAttackEvent = new EventCharacterAfterAttack();
            eventProcessor.ProcessEvent(state, afterAttackEvent);
        }

        private bool IsAttackStillValid(ServerGameState state, EventCharacterAttacks eventAttack)
        {
            // TODO: Process other types of attack (only Minion -> Player attacks are supported the moment)
            var attackingMinion = state.Me.Minions.FirstOrDefault(x => x.Id == eventAttack.Attacker);
            var attackedOpponentFace = state.Opp.Player;
            if (attackingMinion == null)
            {
                logger.Log(LogType.Services, LogSeverity.Info, $"EventCharacterAttack was cancelled because attacker was not found");
                return false;
            }

            if (attackedOpponentFace.Id != eventAttack.Attacked)
            {
                logger.Log(LogType.Services, LogSeverity.Info, $"EventCharacterAttack was cancelled because attacked character was not found");
                return false;
            }

            if (attackedOpponentFace.Health <= 0)
            {
                logger.Log(LogType.Services, LogSeverity.Info, $"EventCharacterAttack was cancelled because attacked character is already dead");
                return false;
            }

            return true;
        }
    }
}
