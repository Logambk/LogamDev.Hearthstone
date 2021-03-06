﻿using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;
using Newtonsoft.Json;

namespace LogamDev.Hearthstone.Arbiter
{
    public class GameArbiter : IGameArbiter
    {
        private readonly IDeckValidator deckValidator;
        private readonly IGameStatePreparator gameStatePreparator;
        private readonly IRuleSet ruleSet;
        private readonly IUserInteractionProcessor userInteractionProcessor;
        private readonly ILogger logger;
        
        private ServerPlayerState player1State;
        private ServerPlayerState player2State;
        private IUserInteractor playerInteractor1;
        private IUserInteractor playerInteractor2;
        private bool isPlayerOneActive;

        private ServerPlayerState ActivePlayerState
        {
            get
            {
                if (isPlayerOneActive)
                {
                    return player1State;
                }
                else
                {
                    return player2State;
                }
            }
        }

        private ServerPlayerState PassivePlayerState
        {
            get
            {
                if (!isPlayerOneActive)
                {
                    return player1State;
                }
                else
                {
                    return player2State;
                }
            }
        }

        private IUserInteractor ActivePlayerInteractor
        {
            get
            {
                if (isPlayerOneActive)
                {
                    return playerInteractor1;
                }
                else
                {
                    return playerInteractor2;
                }
            }
        }

        public GameArbiter(
            IDeckValidator deckValidator,
            IGameStatePreparator gameStatePreparator,
            IRuleSet ruleSet,
            IUserInteractionProcessor userInteractionProcessor,
            ILogger logger)
        {
            this.deckValidator = deckValidator;
            this.gameStatePreparator = gameStatePreparator;
            this.ruleSet = ruleSet;
            this.userInteractionProcessor = userInteractionProcessor;
            this.logger = logger;
        }

        public GameResult HostTheGame(
            PlayerInitializer playerInitializer1,
            PlayerInitializer playerInitializer2,
            IUserInteractor playerInteractor1,
            IUserInteractor playerInteractor2)
        {
            logger.Log(LogType.Arbiter, LogSeverity.Info, "Game started");

            this.playerInteractor1 = playerInteractor1;
            this.playerInteractor2 = playerInteractor2;
            var deckValidation1 = deckValidator.ValidateDeck(playerInitializer1.Deck, playerInitializer1.Class);
            var deckValidation2 = deckValidator.ValidateDeck(playerInitializer2.Deck, playerInitializer2.Class);
            if (!deckValidation1.IsOk || !deckValidation2.IsOk)
            {
                //TODO: figure out where to log the validator messages
                return new GameResult()
                {
                    IsOk = false
                };
            }

            player1State = gameStatePreparator.Initialize(playerInitializer1);
            player2State = gameStatePreparator.Initialize(playerInitializer2);

            //TODO: gamble the right of first turn.
            //TODO: implement mulligan and initial draw here
            //TODO: add service for draws and fatigue
            for (int i = 0; i < ruleSet.HandStartingSize; i++)
            {
                var randomCardIndex = new Random().Next(0, player1State.Deck.Count);
                var card = player1State.Deck[randomCardIndex];
                player1State.Deck.RemoveAt(randomCardIndex);
                player1State.Hand.Add(card);

                var randomCardIndex2 = new Random().Next(0, player2State.Deck.Count);
                var card2 = player2State.Deck[randomCardIndex2];
                player2State.Deck.RemoveAt(randomCardIndex2);
                player2State.Hand.Add(card);
            }
            
            isPlayerOneActive = true;
            var internalTurnNumber = 1;

            while (internalTurnNumber++ < ruleSet.TurnMaxCountPerGame)
            {
                var state = new ServerGameState()
                {
                    Me = ActivePlayerState,
                    Opp = PassivePlayerState
                };

                state.Me.Events.Add(internalTurnNumber, new List<EventBase>());

                logger.Log(LogType.Arbiter, LogSeverity.Info, $"Turn {internalTurnNumber / 2} started for {state.Me.Player.Name}");

                // Add new non-empty mana crystal
                if (state.Me.Mana.PermanentManaCrystals < ruleSet.ManaStorageMaxCrystals)
                {
                    state.Me.Mana.AddManaCrystals(1, false);
                }

                // Refresh Permanent Mana Crystals
                state.Me.Mana.RefreshPermanentManaCrystals();

                //TODO: draw the card from the deck
                var randomCardIndex = new Random().Next(0, state.Me.Deck.Count);
                var card = state.Me.Deck[randomCardIndex];
                state.Me.Deck.RemoveAt(randomCardIndex);
                state.Me.Hand.Add(card);

                //TODO: start of turn events here
                //TODO: update the state to both users
                //TODO: send the events
                var stateForActiveUser = gameStatePreparator.PrepareGameState(state);
                ActivePlayerInteractor.Update(stateForActiveUser);

                //TODO: add time limit for a user to interact
                while (true)
                {
                    var interaction = ActivePlayerInteractor.Interact();
                    var interactionValidation = userInteractionProcessor.ValidateInteraction(stateForActiveUser, interaction);
                    if (!interactionValidation.IsOk)
                    {
                        //TODO: figure out where to log the validator messages
                        return new GameResult()
                        {
                            IsOk = false
                        };
                    }

                    if (interaction is InteractionEndTurn)
                    {
                        break;
                    }

                    //TODO: send the events to other user
                    userInteractionProcessor.ProcessInteraction(state, interaction);
                    if (state.Me.LastTurnEvents.Any(x => x is EventCharacterDied && (x as EventCharacterDied).DiedCharacter == state.Opp.Player.Id))
                    {
                        logger.Log(LogType.Arbiter, LogSeverity.Info, $"{state.Me.Player.Name} Won");
                        logger.Log(LogType.Arbiter, LogSeverity.Info, $"After Game State: {JsonConvert.SerializeObject(state)}");

                        // TODO: find a more approriate way to stop the game
                        return new GameResult()
                        {
                            IsOk = true,
                            IsFirstPlayerWon = isPlayerOneActive,
                            FinalState = state
                        };
                    }

                    stateForActiveUser = gameStatePreparator.PrepareGameState(state);
                    ActivePlayerInteractor.Update(stateForActiveUser);
                }

                // Burn Unused Mana
                state.Me.Mana.BurnTemporaryCrystals();

                //TODO: end of turn events here
                isPlayerOneActive = !isPlayerOneActive;
            }

            return null;
        }
    }
}
