using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Vo.Game;
using Unity;
using Xunit;

namespace LogamDev.Hearthstone.Arbiter.UnitTest
{
    public class ArbiterTest
    {
        [Fact]
        public void ShouldHandleBattleOfTwoRandomMinionFacePlayers()
        {
            var container = new UnityContainer();
            UnityConfig.Register(container);
            Services.UnityConfig.Register(container);

            var arbiter = container.Resolve<IGameArbiter>();
            var playerInitializer1 = new PlayerInitializer();
        }
    }
}
