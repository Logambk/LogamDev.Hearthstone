using LogamDev.Hearthstone.Dto.Interface;
using Unity;

namespace LogamDev.Hearthstone.Dto
{
    public static class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IHearthstoneJsonCardConverter, HearthstoneJsonCardConverter>();
            container.RegisterType<IHearthstoneJsonCardParser, HearthstoneJsonCardParser>();
            container.RegisterType<IDeckPlainTextParser, DeckPlainTextParser>();
        }
    }
}
