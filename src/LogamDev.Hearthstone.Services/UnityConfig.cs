﻿using LogamDev.Hearthstone.Services.Interface;
using Unity;

namespace LogamDev.Hearthstone.Services
{
    public static class UnityConfig
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IDeckValidator, DeckValidator>();
            container.RegisterType<ICardLibrary, CardLibrary>();
            container.RegisterType<IGameStatePreparator, GameStatePreparator>();
            container.RegisterType<IRuleSet, RegularRuleSet>();
            container.RegisterType<IUserInteractionProcessor, UserInteractionProcessor>();
        }
    }
}
