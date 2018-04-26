using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;
using LogamDev.Hearthstone.Vo.Interaction;

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

        public List<GameEventBase> ProcessInteraction(InternalSide internalSideActivePlayer, InternalSide opponentSide, InteractionBase userInteraction)
        {
            if (userInteraction is InteractionPlayCard)
            {
                return ProcessPlayCard(internalSideActivePlayer, opponentSide, userInteraction as InteractionPlayCard);
            }
            else if (userInteraction is InteractionAttack)
            {
                return ProcessAttack(internalSideActivePlayer, opponentSide, userInteraction as InteractionAttack);
            }
            else if (userInteraction is InteractionEndTurn)
            {
                //TODO: support interaction
                return new List<GameEventBase>();
            }
            else
            {
                //TODO: handle unknown type
                return new List<GameEventBase>();
            }
        }

        private List<GameEventBase> ProcessAttack(InternalSide internalSideActivePlayer, InternalSide opponentSide, InteractionAttack interactionAttack)
        {
            var events = new List<GameEventBase>();

            if (interactionAttack.Attacker != null && interactionAttack.Target == null)
            {
                // only handle minion attack in the face at the moment
                events.Add(new GameEventAttack() { Attacker = interactionAttack.Attacker, Target = interactionAttack.Target });
                var attackingMinion = internalSideActivePlayer.Minions.First(x => x.Id == interactionAttack.Attacker);

                logger.Log(LogType.Services, LogSeverity.Info, $"{attackingMinion.Card.Name} attacks {opponentSide.Player.Name} for {attackingMinion.Attack} hp");

                opponentSide.Player.Health -= attackingMinion.Attack;

                logger.Log(LogType.Services, LogSeverity.Info, $"{opponentSide.Player.Name} Health reduced to {opponentSide.Player.Health} hp");

                if (opponentSide.Player.Health <= 0)
                {
                    events.Add(new GameEventPlayerDeath());
                }
            }

            //TODO: handle other types of attack targets
            return events;
        }

        private List<GameEventBase> ProcessPlayCard(InternalSide internalSideActivePlayer, InternalSide opponentSide, InteractionPlayCard userInteractionPlayCard)
        {
            var events = new List<GameEventBase>();
            var card = internalSideActivePlayer.Hand.First(x => x.Id == userInteractionPlayCard.CardId);

            logger.Log(LogType.Services, LogSeverity.Info, $"{internalSideActivePlayer.Player.Name} plays {card.Name} for {card.Cost} mana");

            //TODO: add mana service to handle mana payments
            var manaNeeded = card.Cost;
            if (internalSideActivePlayer.Player.TemporaryManaCrystals > 0)
            {
                var appliedMana = Math.Min(manaNeeded, internalSideActivePlayer.Player.TemporaryManaCrystals);
                manaNeeded -= appliedMana;
                internalSideActivePlayer.Player.TemporaryManaCrystals -= appliedMana;
            }

            if (manaNeeded > 0)
            {
                internalSideActivePlayer.Player.UsedPermanentManaCrystals += manaNeeded;
            }

            //TODO: handle opponent secrets somewhere here
            internalSideActivePlayer.Hand.Remove(card);
            events.Add(new GameEventPlayCard() { Dbfid = card.DbfId });

            switch (card.Type)
            {
                case CardType.Spell:
                    break;
                case CardType.Minion:
                    var minion = new Minion(card as CardMinion);
                    logger.Log(LogType.Services, LogSeverity.Info, $"{minion.Card.Name} is summoned");
                    internalSideActivePlayer.Minions.Insert(userInteractionPlayCard.MinionPosition.Value, minion);
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

        private ValidationResult ValidatePlayCard(GameState currentState, InteractionPlayCard playCardInteraction)
        {
            var validationResult = new ValidationResult()
            {
                IsOk = true,
                Messages = new List<string>()
            };

            if (playCardInteraction.CardId == null)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Failed to play card: CardId is null");
                return validationResult;
            }

            var matchedCards = currentState.Hand.FindAll(x => x.Id == playCardInteraction.CardId);
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
            var availableMana = currentState.You.TotalPermanentManaCrystals - currentState.You.UsedPermanentManaCrystals + currentState.You.TemporaryManaCrystals;
            if (cardInHand.Cost > availableMana)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Failed to play card: Card costs {cardInHand.Cost} but only {availableMana} mana is available");
                return validationResult;
            }

            if (cardInHand.Type == Vo.Enum.CardType.Minion)
            {
                if (currentState.YourMinions.Count >= ruleSet.FieldMaxMinionsAtSide)
                {
                    validationResult.IsOk = false;
                    validationResult.Messages.Add($"Failed to play card: You cannot have more then {ruleSet.FieldMaxMinionsAtSide} minions");
                    return validationResult;
                }

                if (playCardInteraction.MinionPosition == null)
                {
                    validationResult.IsOk = false;
                    validationResult.Messages.Add($"Failed to play card: Required minion position was not provided");
                    return validationResult;
                }

                if (playCardInteraction.MinionPosition < 0 || playCardInteraction.MinionPosition > currentState.YourMinions.Count)
                {
                    validationResult.IsOk = false;
                    validationResult.Messages.Add($"Failed to play card: Minion position incorrect. Expected betwen 0 and {currentState.YourMinions.Count}, but {playCardInteraction.MinionPosition} was provided");
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
