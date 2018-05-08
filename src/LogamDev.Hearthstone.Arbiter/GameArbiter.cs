using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Arbiter
{
    public class GameArbiter : IGameArbiter
    {
        private readonly IDeckValidator deckValidator;
        private readonly IGameStatePreparator gameStatePreparator;
        private readonly IRuleSet ruleSet;
        private readonly IUserInteractionProcessor userInteractionProcessor;
        private readonly ILogger logger;
        
        private InternalState player1State;
        private InternalState player2State;
        private IUserInteractor playerInteractor1;
        private IUserInteractor playerInteractor2;
        private bool isPlayerOneActive;

        private InternalState ActivePlayerState
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

        private InternalState PassivePlayerState
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

        public GameResult StartGame(
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
            var turnNumberMax = 400;

            while (internalTurnNumber++ < turnNumberMax)
            {
                logger.Log(LogType.Arbiter, LogSeverity.Info, $"Turn {internalTurnNumber / 2} started for {ActivePlayerState.Player.Name}");

                var events = new List<EventBase>();
               
                // Add new non-empty mana crystal
                if (ActivePlayerState.Mana.PermanentManaCrystals < ruleSet.ManaStorageMaxCrystals)
                {
                    ActivePlayerState.Mana.AddManaCrystals(1, false);
                }

                // Refresh Permanent Mana Crystals
                ActivePlayerState.Mana.RefreshPermanentManaCrystals();

                //TODO: draw the card from the deck
                var randomCardIndex = new Random().Next(0, ActivePlayerState.Deck.Count);
                var card = ActivePlayerState.Deck[randomCardIndex];
                ActivePlayerState.Deck.RemoveAt(randomCardIndex);
                ActivePlayerState.Hand.Add(card);

                //TODO: start of turn events here
                //TODO: update the state to both users
                //TODO: send the events
                var stateForActiveUser = gameStatePreparator.PrepareGameState(ActivePlayerState, PassivePlayerState, events);
                ActivePlayerInteractor.Update(new GameStateUpdate() { NewState = stateForActiveUser });

                //TODO: add time limit for a user to interact
                while (true)
                {
                    var interaction = ActivePlayerInteractor.Interact();
                    var interactionValidation = userInteractionProcessor.ValidateUserInteraction(stateForActiveUser, interaction);
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
                    var newEvents = userInteractionProcessor.ProcessInteraction(ActivePlayerState, PassivePlayerState, interaction);
                    events.AddRange(newEvents);
                    if (events.Any(x => x is EventPlayerDeath))
                    {
                        logger.Log(LogType.Arbiter, LogSeverity.Info, $"{ActivePlayerState.Player.Name} Won");

                        // TODO: find a more approriate way to stop the game
                        return new GameResult()
                        {
                            IsOk = true,
                            IsFirstPlayerWon = isPlayerOneActive
                        };
                    }

                    stateForActiveUser = gameStatePreparator.PrepareGameState(ActivePlayerState, PassivePlayerState, events);
                    ActivePlayerInteractor.Update(new GameStateUpdate() { Events = events, NewState = stateForActiveUser });
                }

                // Burn Unused Mana
                ActivePlayerState.Mana.BurnTemporaryCrystals();

                //TODO: end of turn events here
                isPlayerOneActive = !isPlayerOneActive;
            }

            return null;
        }
    }
}
