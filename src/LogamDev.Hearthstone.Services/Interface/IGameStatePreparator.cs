using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IGameStatePreparator
    {
        GameState PrepareGameState(InternalSide activeUser, InternalSide opponent);
    }
}
