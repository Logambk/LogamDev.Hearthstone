using System;
using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Services
{
    public class InternalSideInitializer : IInternalSideInitializer
    {
        private readonly IRuleSet ruleSet;

        public InternalSideInitializer(IRuleSet ruleSet)
        {
            this.ruleSet = ruleSet;
        }

        public InternalSide Initialize(PlayerInitializer playerInitializer)
        {
            var state = new InternalSide()
            {
                Deck = playerInitializer.Deck,
                Hand = new List<CardBase>(),
                Minions = new List<Minion>(),
                Player = new Player()
                {
                    Armor = 0,
                    EquipedWeapon = null,
                    Health = ruleSet.PlayerStartingHealth,
                    TemporaryManaCrystals = 0,
                    TotalPermanentManaCrystals = ruleSet.PlayerStartingManaCrystals,
                    UsedPermanentManaCrystals = 0,
                }
            };

            foreach (var card in state.Deck)
            {
                card.Id = Guid.NewGuid();
            }

            return state;
        }
    }
}
