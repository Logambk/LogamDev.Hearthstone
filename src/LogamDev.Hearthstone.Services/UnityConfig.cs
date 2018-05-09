using LogamDev.Hearthstone.Services.Event;
using LogamDev.Hearthstone.Services.Interaction;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using Unity;

namespace LogamDev.Hearthstone.Services
{
    public static class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<ICardLibrary, CardLibrary>();
            container.RegisterType<IDeckValidator, DeckValidator>();
            container.RegisterType<IEventProcessor, EventProcessor>();
            container.RegisterType<IGameStatePreparator, GameStatePreparator>();
            container.RegisterType<ILogger, TextFileLogger>();
            container.RegisterType<IRuleSet, RegularRuleSet>();
            container.RegisterType<IUserInteractionProcessor, UserInteractionProcessor>();
        }
    }
}
