using System;
using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services
{
    public class GameStatePreparator : IGameStatePreparator
    {
        private readonly IRuleSet ruleSet;

        public GameStatePreparator(IRuleSet ruleSet)
        {
            this.ruleSet = ruleSet;
        }

        public GameState PrepareGameState(InternalState me, InternalState opp, List<EventBase> thisTurnEvents)
        {
            return new GameState()
            {
                Me = me,
                Opp = new ExternalState()
                {
                    Player = opp.Player,
                    Minions = opp.Minions,
                    ManaStorage = opp.Mana,
                    DeckSize = opp.Deck.Count,
                    HandSize = opp.Hand.Count
                },
                ThisTurnEvents = thisTurnEvents,
            };
        }

        public InternalState Initialize(PlayerInitializer playerInitializer)
        {
            var state = new InternalState()
            {
                Deck = playerInitializer.Deck,
                Hand = new List<CardBase>(),
                Minions = new List<Minion>(),
                Player = new Player(playerInitializer.Name, playerInitializer.Class, ruleSet.PlayerStartingHealth),
                Mana = new ManaStorage(ruleSet.ManaStorageCrystalsAtStart)
            };

            foreach (var card in state.Deck)
            {
                card.Id = Guid.NewGuid();
            }

            return state;
        }
    }
}
