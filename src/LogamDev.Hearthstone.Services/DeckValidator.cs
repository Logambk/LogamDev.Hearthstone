using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services
{
    public class DeckValidator : IDeckValidator
    {
        private readonly IRuleSet ruleSet;

        public DeckValidator(IRuleSet ruleSet)
        {
            this.ruleSet = ruleSet;
        }

        //TODO: validate format as well
        public ValidationResult ValidateDeck(Deck deck, CardClass cardClass)
        {
            var validationResult = new ValidationResult()
            {
                IsOk = true,
                Messages = new List<string>()
            };

            if (deck.Class != cardClass)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Deck class {deck.Class} doesn't match the hero class {cardClass}");
            }

            if (deck.Cards.Sum(x => x.Value) != ruleSet.DeckSize)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Wrong deck size. Expected {ruleSet.DeckSize}, but found {deck.Cards.Sum(x => x.Value)}");
            }

            if (cardClass == CardClass.Neutral)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Wrong class. Player cannot have 'Neutral' as a class");
            }

            var grouppedClasses = deck.Cards.Keys.Where(x => x.Class != CardClass.Neutral && x.Class != cardClass).GroupBy(x => x.Class);
            if (grouppedClasses.Any())
            {
                validationResult.IsOk = false;
                var logData = string.Join("; ", grouppedClasses.Select(x => $"Class: {x.Key}, Count: {x.Count()}"));
                validationResult.Messages.Add($"Wrong deck. Expected only cards of {cardClass} and {CardClass.Neutral}, but found other classes: {logData}");
            }

            var legs = deck.Cards.Where(x => x.Key.Rarity == CardRarity.Legendary && x.Value > ruleSet.DeckMaxLegendaryCards);
            if (legs.Any())
            {
                validationResult.IsOk = false;
                var logData = string.Join("; ", legs.Select(x => $"Id: {x.Key}, Count: {x.Value}"));
                validationResult.Messages.Add($"Wrong deck. Expected not more then {ruleSet.DeckMaxLegendaryCards} copy(ies) of Legendary cards. Violation: {logData}");
            }

            var nonLegs = deck.Cards.Where(x => x.Key.Rarity != CardRarity.Legendary && x.Value > ruleSet.DeckMaxNonLegendaryCards);
            if (nonLegs.Any())
            {
                validationResult.IsOk = false;
                var logData = string.Join("; ", nonLegs.Select(x => $"Id: {x.Key}, Count: {x.Value}"));
                validationResult.Messages.Add($"Wrong deck. Expected not more then {ruleSet.DeckMaxNonLegendaryCards} copy(ies) of Non-Legendary cards. Violation: {logData}");
            }

            return validationResult;
        }
    }
}
