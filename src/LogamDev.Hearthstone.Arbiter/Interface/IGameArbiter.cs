using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Arbiter.Interface
{
    public interface IGameArbiter
    {
        GameResult StartGame(
            PlayerInitializer playerInitializer1,
            PlayerInitializer playerInitializer2,
            IUserInteractor playerInteractor1,
            IUserInteractor playerINteractor2);
    }
}
