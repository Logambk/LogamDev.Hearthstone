using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IGameStatePreparator
    {
        GameState PrepareGameState(FullGameState fullState);
        InternalSide Initialize(PlayerInitializer playerInitializer);
    }
}
