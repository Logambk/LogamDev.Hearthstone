﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public GameState PrepareGameState(FullGameState fullState)
        {
            return new GameState()
            {
                Me = fullState.Me,
                Opp = new ExternalSide()
                {
                    Player = fullState.Opp.Player,
                    Minions = fullState.Opp.Minions,
                    ManaStorage = fullState.Opp.Mana,
                    DeckSize = fullState.Opp.Deck.Count,
                    HandSize = fullState.Opp.Hand.Count,
                    VisibleEvents = PrepareEventsForExternalUser(fullState.Opp.Events)
                }
            };
        }

        private Dictionary<int, List<EventBase>> PrepareEventsForExternalUser(Dictionary<int, List<EventBase>> events)
        {
            var newEvents = new Dictionary<int, List<EventBase>>();
            foreach (var kvp in events)
            {
                //TODO: think about how to "hide" the events behind the "substitute events"
                newEvents.Add(kvp.Key, kvp.Value.Where(x => x.IsHiddenToOpponent == false).ToList());
            }

            return newEvents;
        }

        public InternalSide Initialize(PlayerInitializer playerInitializer)
        {
            var state = new InternalSide()
            {
                Deck = playerInitializer.Deck,
                Hand = new List<CardBase>(),
                Minions = new List<Minion>(),
                Player = new Player(playerInitializer.Name, playerInitializer.Class, ruleSet.PlayerStartingHealth),
                Mana = new ManaStorage(ruleSet.ManaStorageCrystalsAtStart),
                MinionOrderNumber = 0,
                Triggers = new TriggerStorage(),
                Events = new Dictionary<int, List<EventBase>>()
            };

            //TODO: add proper class for deck
            foreach (var card in state.Deck)
            {
                card.Id = Guid.NewGuid();
            }

            return state;
        }
    }
}
