using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Services
{
    public class UserInteractionProcessor : IUserInteractionProcessor
    {
        private readonly IRuleSet ruleSet;

        public UserInteractionProcessor(IRuleSet ruleSet)
        {
            this.ruleSet = ruleSet;
        }

        public List<GameEvent> ProcessInteraction(InternalSide internalSideActivePlayer, InternalSide opponentSide, UserInteractionBase userInteraction)
        {
            if (userInteraction is UserInteractionPlayCard)
            {
                return ProcessPlayCard(internalSideActivePlayer, opponentSide, userInteraction as UserInteractionPlayCard);
            }
            else if (userInteraction is UserInteractionAttack)
            {
                //TODO: support interaction
                return new List<GameEvent>();
            }
            else if (userInteraction is UserInteractionEndTurn)
            {
                //TODO: support interaction
                return new List<GameEvent>();
            }
            else
            {
                //TODO: handle unknown type
                return new List<GameEvent>();
            }
        }

        private List<GameEvent> ProcessPlayCard(InternalSide internalSideActivePlayer, InternalSide opponentSide, UserInteractionPlayCard userInteractionPlayCard)
        {
            var card = internalSideActivePlayer.Hand.First(x => x.Id == userInteractionPlayCard.CardId);
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

            switch (card.Type)
            {
                case CardType.Spell:
                    break;
                case CardType.Minion:
                    var minion = new Minion(card as CardMinion);
                    internalSideActivePlayer.Minions.Insert(userInteractionPlayCard.MinionPosition.Value, minion);
                    break;
                case CardType.Weapon:
                    break;
                case CardType.Hero:
                    break;
            }

            return new List<GameEvent>();
        }

        public ValidationResult ValidateUserInteraction(GameState currentState, UserInteractionBase interaction)
        {
            if (interaction is UserInteractionPlayCard)
            {
                return ValidatePlayCard(currentState, interaction as UserInteractionPlayCard);
            }
            else if (interaction is UserInteractionAttack)
            {
                return ValidateAttack(currentState, interaction as UserInteractionAttack);
            }
            else if (interaction is UserInteractionEndTurn)
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

        private ValidationResult ValidatePlayCard(GameState currentState, UserInteractionPlayCard playCardInteraction)
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

        private ValidationResult ValidateAttack(GameState currentState, UserInteractionAttack attackInteraction)
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
