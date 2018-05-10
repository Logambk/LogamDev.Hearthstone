using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IDeckConverter
    {
        Deck Convert(Dto.Deck deckDto);
    }
}
