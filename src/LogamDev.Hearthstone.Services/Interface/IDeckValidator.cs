using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IDeckValidator
    {
        ValidationResult ValidateDeck(Deck deck, CardClass cardClass);
    }
}
