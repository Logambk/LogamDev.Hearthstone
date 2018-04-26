using System;
using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;

namespace LogamDev.Hearthstone.Services
{
    public class GameStatePreparator : IGameStatePreparator
    {
        private readonly IRuleSet ruleSet;

        public GameStatePreparator(IRuleSet ruleSet)
        {
            this.ruleSet = ruleSet;
        }

        public GameState PrepareGameState(InternalSide activeUser, InternalSide opponent, List<GameEventBase> thisTurnEvents)
        {
            return new GameState()
            {
                Hand = activeUser.Hand,
                Opponent = opponent.Player,
                OpponentDeckSize = opponent.Deck.Count,
                OpponentHandSize = opponent.Hand.Count,
                OpponentMinions = opponent.Minions,
                You = activeUser.Player,
                YourDeckSize = activeUser.Deck.Count,
                YourMinions = activeUser.Minions,
                ThisTurnEvents = thisTurnEvents
            };
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
                    Class = playerInitializer.Class,
                    Name = playerInitializer.Name
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
