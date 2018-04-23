using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Services
{
    public class DeckValidator : IDeckValidator
    {
        private readonly IRuleSet ruleSet;

        public DeckValidator(IRuleSet ruleSet)
        {
            this.ruleSet = ruleSet;
        }

        public ValidationResult ValidateDeck(List<CardBase> cards, CardClass cardClass)
        {
            var validationResult = new ValidationResult()
            {
                IsOk = true,
                Messages = new List<string>()
            };

            if (cards.Count != ruleSet.DeckSize)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Wrong deck size. Expected {ruleSet.DeckSize}, but found {cards.Count}");
            }

            if (cardClass == CardClass.Neutral)
            {
                validationResult.IsOk = false;
                validationResult.Messages.Add($"Wrong class. Player cannot have 'Neutral' as a main class");
            }

            var grouppedClasses = cards.Where(x => x.Class != CardClass.Neutral && x.Class != cardClass).GroupBy(x => x.Class);
            if (grouppedClasses.Any())
            {
                validationResult.IsOk = false;
                var logData = string.Join("; ", grouppedClasses.Select(x => $"Class: {x.Key}, Count: {x.Count()}"));
                validationResult.Messages.Add($"Wrong deck. Expected only cards of {cardClass} and {CardClass.Neutral}, but found other classes: {logData}");
            }

            var groupedLegendaries = cards.Where(x => x.Rarity == CardRarity.Legendary).GroupBy(x => x.DbfId).Where(x => x.Count() > ruleSet.DeckMaxLegendaryCards);
            if (groupedLegendaries.Any())
            {
                validationResult.IsOk = false;
                var logData = string.Join("; ", groupedLegendaries.Select(x => $"Id: {x.Key}, Count: {x.Count()}"));
                validationResult.Messages.Add($"Wrong deck. Expected not more then {ruleSet.DeckMaxLegendaryCards} copy(ies) of Legendary cards. Violation: {logData}");
            }

            var groupedNonLegendaries = cards.Where(x => x.Rarity != CardRarity.Legendary).GroupBy(x => x.DbfId).Where(x => x.Count() > ruleSet.DeckMaxNonLegendaryCards);
            if (groupedNonLegendaries.Any())
            {
                validationResult.IsOk = false;
                var logData = string.Join("; ", groupedNonLegendaries.Select(x => $"Id: {x.Key}, Count: {x.Count()}"));
                validationResult.Messages.Add($"Wrong deck. Expected not more then {ruleSet.DeckMaxNonLegendaryCards} copy(ies) of Non-Legendary cards. Violation: {logData}");
            }

            return validationResult;
        }
    }
}
