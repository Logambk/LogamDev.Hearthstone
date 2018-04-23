using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IDeckValidator
    {
        ValidationResult ValidateDeck(List<CardBase> cards, CardClass cardClass);
    }
}
