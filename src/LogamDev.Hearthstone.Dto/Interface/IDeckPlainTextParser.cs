using System.Collections.Generic;

namespace LogamDev.Hearthstone.Dto.Interface
{
    public interface IDeckPlainTextParser
    {
        List<string> ParseDeck(string text);
    }
}
