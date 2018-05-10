using LogamDev.Hearthstone.Dto.Interface;
using Unity;
using Unity.Lifetime;

namespace LogamDev.Hearthstone.Dto
{
    public static class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IHearthstoneJsonCardParser, HearthstoneJsonCardParser>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeckPlainTextParser, DeckPlainTextParser>(new ContainerControlledLifetimeManager());
        }
    }
}
