using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services
{
    public class UserInteractionProcessor : IUserInteractionProcessor
    {
        private readonly IRuleSet ruleSet;
        private readonly ILogger logger;

        public UserInteractionProcessor(IRuleSet ruleSet, ILogger logger)
        {
            this.ruleSet = ruleSet;
            this.logger = logger;
        }

        public List<GameEventBase> ProcessInteraction(InternalState me, InternalState opp, InteractionBase interaction)
        {
            switch (interaction.Type)
            {
                case InteractionType.Attack:
                    return ProcessAttack(me, opp, interaction as InteractionAttack);

                case InteractionType.PlayCard:
                    return ProcessPlayCard(me, opp, interaction as InteractionPlayCard);

                case InteractionType.EndTurn:
                    //TODO: support interaction
                    return new List<GameEventBase>();
            }

            //TODO: handle unknown type
            return new List<GameEventBase>();
        }

        private List<GameEventBase> ProcessAttack(InternalState me, InternalState opp, InteractionAttack interactionAttack)
        {
            var events = new List<GameEventBase>();

            if (interactionAttack.Attacker != null && interactionAttack.Target == null)
            {
                // only handle minion attack in the face at the moment
                events.Add(new GameEventAttack() { Attacker = interactionAttack.Attacker, Target = interactionAttack.Target });
                var attackingMinion = me.Minions.First(x => x.Id == interactionAttack.Attacker);

                logger.Log(LogType.Services, LogSeverity.Info, $"{attackingMinion.Card.Name} attacks {opp.Player.Name} for {attackingMinion.Attack} hp");

                opp.Player.Health -= attackingMinion.Attack;

                logger.Log(LogType.Services, LogSeverity.Info, $"{opp.Player.Name} Health reduced to {opp.Player.Health} hp");

                if (opp.Player.Health <= 0)
                {
                    events.Add(new GameEventPlayerDeath());
                }
            }

            //TODO: handle other types of attack targets
            return events;
        }

        private List<GameEventBase> ProcessPlayCard(InternalState me, InternalState opp, InteractionPlayCard interactionPlayCard)
        {
            var events = new List<GameEventBase>();
            var card = me.Hand.First(x => x.Id == interactionPlayCard.CardId);

            logger.Log(LogType.Services, LogSeverity.Info, $"{me.Player.Name} plays {card.Name} for {card.Cost} mana");

            me.Mana.SpendMana(card.Cost);

            //TODO: handle opponent secrets somewhere here
            me.Hand.Remove(card);
            events.Add(new GameEventPlayCard() { Dbfid = card.DbfId });

            switch (card.Type)
            {
                case CardType.Spell:
                    break;
                case CardType.Minion:
                    var minion = new Minion(card as CardMinion);
                    logger.Log(LogType.Services, LogSeverity.Info, $"{minion.Card.Name} is summoned");
                    me.Minions.Insert(interactionPlayCard.MinionPosition.Value, minion);
                    events.Add(new GameEventSummon() { MinionId = minion.Id });
                    break;
                case CardType.Weapon:
                    break;
                case CardType.Hero:
                    break;
            }

            return events;
        }

        public ValidationResult ValidateUserInteraction(GameState currentState, InteractionBase interaction)
        {
            if (interaction is InteractionPlayCard)
            {
                return ValidatePlayCard(currentState, interaction as InteractionPlayCard);
            }
            else if (interaction is InteractionAttack)
            {
                return ValidateAttack(currentState, interaction as InteractionAttack);
            }
            else if (interaction is InteractionEndTurn)
            {
                return ValidateEndTurn(currentState);
            }
            else
            {
                return new ValidationResult()
                {
                    IsOk = false,
                    Messages = new List<string>() { $"Unknown Interaction of type {interaction.GetType()} encountered" }
                };
            }
        }

        private ValidationResult ValidatePlayCard(GameState state, InteractionPlayCard interactionPlayCard)
        {
            var validationResult = new ValidationResult()
            {
                IsOk = true,
                Messages = new List<string>()
            };

            if (interactionPlayCard.CardId == null)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Failed to play card: CardId is null");
                return validationResult;
            }

            var matchedCards = state.Me.Hand.FindAll(x => x.Id == interactionPlayCard.CardId);
            if (matchedCards.Count > 1)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Failed to play card: There are more then one card with same CardId");
                return validationResult;
            }

            if (matchedCards.Count == 0)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Failed to play card: Card was not found in the hand");
                return validationResult;
            }

            var cardInHand = matchedCards[0];

            //TODO: add service for handling mana costs
            if (cardInHand.Cost > state.Me.Mana.AvailableManaThisTurn)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Failed to play card: Card costs {cardInHand.Cost} but only {state.Me.Mana.AvailableManaThisTurn} mana is available");
                return validationResult;
            }

            if (cardInHand.Type == CardType.Minion)
            {
                if (state.Me.Minions.Count >= ruleSet.FieldMaxMinionsAtSide)
                {
                    validationResult.IsOk = false;
                    validationResult.Messages.Add($"Failed to play card: You cannot have more then {ruleSet.FieldMaxMinionsAtSide} minions");
                    return validationResult;
                }

                if (interactionPlayCard.MinionPosition == null)
                {
                    validationResult.IsOk = false;
                    validationResult.Messages.Add($"Failed to play card: Required minion position was not provided");
                    return validationResult;
                }

                if (interactionPlayCard.MinionPosition < 0 || interactionPlayCard.MinionPosition > state.Me.Minions.Count)
                {
                    validationResult.IsOk = false;
                    validationResult.Messages.Add($"Failed to play card: Minion position incorrect. Expected betwen 0 and {state.Me.Minions.Count}, but {interactionPlayCard.MinionPosition} was provided");
                    return validationResult;
                }
            }

            //TODO: implement the rest of the validation
            return new ValidationResult() { IsOk = true };
        }

        private ValidationResult ValidateAttack(GameState currentState, InteractionAttack attackInteraction)
        {
            //TODO: implement validation
            return new ValidationResult() { IsOk = true };
        }

        private ValidationResult ValidateEndTurn(GameState currentState)
        {
            //TODO: implement validation
            return new ValidationResult() { IsOk = true };
        }
    }
}
