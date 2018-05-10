using System.Collections.Generic;
using System.Text.RegularExpressions;
using LogamDev.Hearthstone.Dto.Interface;

namespace LogamDev.Hearthstone.Dto
{
    public class DeckPlainTextParser : IDeckPlainTextParser
    {
        private readonly Regex nameRegex = new Regex("### (.+)");
        private readonly Regex classRegex = new Regex("# Class: (.+)");
        private readonly Regex formatRegex = new Regex("# Format: (.+)");
        private readonly Regex cardRegex = new Regex(@"# (\d)x \(\d+\) ([^\r\n]+)");

        public Deck ParseDeck(string text)
        {
            var deck = new Deck();

            var nameMatch = nameRegex.Match(text);
            deck.Name = nameMatch.Success ? nameMatch.Groups[1].Captures[0].Value : null;
            var classMatch = classRegex.Match(text);
            deck.Class = classMatch.Success ? classMatch.Groups[1].Captures[0].Value : null;
            var formatMatch = formatRegex.Match(text);
            deck.Format = formatMatch.Success ? formatMatch.Groups[1].Captures[0].Value : null;

            deck.Cards = new Dictionary<string, int>();
            var matches = cardRegex.Matches(text);
            foreach (Match match in matches)
            {
                var count = int.Parse(match.Groups[1].Captures[0].Value);
                var cardName = match.Groups[2].Captures[0].Value;
                if (!deck.Cards.ContainsKey(cardName))
                {
                    deck.Cards.Add(cardName, count);
                }
            }

            return deck;
        }
    }
}
