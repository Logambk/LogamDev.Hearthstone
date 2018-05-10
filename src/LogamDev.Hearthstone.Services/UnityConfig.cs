using LogamDev.Hearthstone.Services.Conversion;
using LogamDev.Hearthstone.Services.Event;
using LogamDev.Hearthstone.Services.Interaction;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using Unity;
using Unity.Lifetime;

namespace LogamDev.Hearthstone.Services
{
    public static class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<ICardLibrary, CardLibrary>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeckConverter, DeckConverter>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeckValidator, DeckValidator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventProcessor, EventProcessor>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGameStatePreparator, GameStatePreparator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHearthstoneJsonCardConverter, HearthstoneJsonCardConverter>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILogger, TextFileLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRuleSet, RegularRuleSet>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserInteractionProcessor, UserInteractionProcessor>(new ContainerControlledLifetimeManager());
        }
    }
}
