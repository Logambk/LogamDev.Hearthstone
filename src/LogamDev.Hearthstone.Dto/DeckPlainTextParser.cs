using System.Collections.Generic;
using System.Text.RegularExpressions;
using LogamDev.Hearthstone.Dto.Interface;

namespace LogamDev.Hearthstone.Dto
{
    public class DeckPlainTextParser : IDeckPlainTextParser
    {
        public List<string> ParseDeck(string text)
        {
            var cardNames = new List<string>();
            var cardRegex = new Regex(@"# (\d)x \(\d+\) ([^\r\n]+)");
            var matches = cardRegex.Matches(text);
            foreach (Match match in matches)
            {
                var count = int.Parse(match.Groups[1].Captures[0].Value);
                var cardName = match.Groups[2].Captures[0].Value;
                for (int i = 0; i < count; i++)
                {
                    cardNames.Add(cardName);
                }
            }

            return cardNames;
        }
    }
}
