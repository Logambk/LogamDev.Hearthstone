using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Interaction
{
    public class PlayCardProcessor
    {
        private readonly ILogger logger;
        private readonly IRuleSet ruleSet;
        private readonly IEventProcessor eventProcessor;

        public PlayCardProcessor(ILogger logger, IRuleSet ruleSet, IEventProcessor eventProcessor)
        {
            this.logger = logger;
            this.ruleSet = ruleSet;
            this.eventProcessor = eventProcessor;
        }

        public List<EventBase> ProcessPlayCard(ServerGameState fullState, InteractionPlayCard interactionPlayCard)
        {
            var me = fullState.Me;
            var opp = fullState.Opp;
            var eventsToProcess = new List<EventBase>();

            var card = me.Hand.First(x => x.Id == interactionPlayCard.CardId);
            logger.Log(LogType.Services, LogSeverity.Info, $"{me.Player.Name} plays {card.Name} for {card.Cost} mana");
            me.Mana.SpendMana(card.Cost);
            me.Hand.Remove(card);

            var newEvent = new EventCardPlayed() { Card = card, MinionPosition = interactionPlayCard.MinionPosition };
            var actualEvents = eventProcessor.ProcessEvent(fullState, newEvent);
            return actualEvents;
        }

        public ValidationResult ValidatePlayCard(ClientGameState state, InteractionPlayCard interactionPlayCard)
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
    }
}
