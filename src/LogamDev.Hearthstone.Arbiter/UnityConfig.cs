using LogamDev.Hearthstone.Arbiter.Interface;
using Unity;
using Unity.Lifetime;

namespace LogamDev.Hearthstone.Arbiter
{
    public static class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IGameArbiter, GameArbiter>(new ContainerControlledLifetimeManager());
        }
    }
}
