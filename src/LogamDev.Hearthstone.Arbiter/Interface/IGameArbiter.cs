using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Arbiter.Interface
{
    public interface IGameArbiter
    {
        GameResult HostTheGame(
            PlayerInitializer playerInitializer1,
            PlayerInitializer playerInitializer2,
            IUserInteractor playerInteractor1,
            IUserInteractor playerInteractor2);
    }
}
