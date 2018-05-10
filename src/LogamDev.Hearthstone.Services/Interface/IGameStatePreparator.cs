using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IGameStatePreparator
    {
        ClientGameState PrepareGameState(ServerGameState fullState);
        ServerPlayerState Initialize(PlayerInitializer playerInitializer);
    }
}
