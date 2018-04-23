using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IInternalSideInitializer
    {
        InternalSide Initialize(PlayerInitializer playerInitializer);
    }
}
