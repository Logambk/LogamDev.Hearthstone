using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Services.Log;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Arbiter
{
    public class GameArbiter : IGameArbiter
    {
        private readonly IDeckValidator deckValidator;
        private readonly IGameStatePreparator gameStatePreparator;
        private readonly IRuleSet ruleSet;
        private readonly IUserInteractionProcessor userInteractionProcessor;
        private readonly ILogger logger;
        
        private InternalSide player1Side;
        private InternalSide player2Side;
        private IUserInteractor playerInteractor1;
        private IUserInteractor playerInteractor2;
        private bool isPlayerOneActive;

        private InternalSide ActivePlayerSide
        {
            get
            {
                if (isPlayerOneActive)
                {
                    return player1Side;
                }
                else
                {
                    return player2Side;
                }
            }
        }

        private InternalSide PassivePlayerSide
        {
            get
            {
                if (!isPlayerOneActive)
                {
                    return player1Side;
                }
                else
                {
                    return player2Side;
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

            player1Side = gameStatePreparator.Initialize(playerInitializer1);
            player2Side = gameStatePreparator.Initialize(playerInitializer2);

            //TODO: gamble the right of first turn.
            //TODO: implement mulligan and initial draw here
            //TODO: add service for draws and fatigue
            for (int i = 0; i < ruleSet.HandStartingSize; i++)
            {
                var randomCardIndex = new Random().Next(0, player1Side.Deck.Count);
                var card = player1Side.Deck[randomCardIndex];
                player1Side.Deck.RemoveAt(randomCardIndex);
                player1Side.Hand.Add(card);

                var randomCardIndex2 = new Random().Next(0, player2Side.Deck.Count);
                var card2 = player2Side.Deck[randomCardIndex2];
                player2Side.Deck.RemoveAt(randomCardIndex2);
                player2Side.Hand.Add(card);
            }
            
            isPlayerOneActive = true;
            var internalTurnNumber = 1;
            var turnNumberMax = 400;

            while (internalTurnNumber++ < turnNumberMax)
            {
                logger.Log(LogType.Arbiter, LogSeverity.Info, $"Turn {internalTurnNumber / 2} started for {ActivePlayerSide.Player.Name}");

                var events = new List<GameEventBase>();
                //TODO: turn structure here
                //TODO: server events here
                if (ActivePlayerSide.Player.TotalPermanentManaCrystals < ruleSet.PlayerMaxManaCrystals)
                {
                    ActivePlayerSide.Player.TotalPermanentManaCrystals++;
                }

                //TODO: draw the card from the deck
                var randomCardIndex = new Random().Next(0, ActivePlayerSide.Deck.Count);
                var card = ActivePlayerSide.Deck[randomCardIndex];
                ActivePlayerSide.Deck.RemoveAt(randomCardIndex);
                ActivePlayerSide.Hand.Add(card);

                //TODO: start of turn events here
                //TODO: update the state to both users
                //TODO: send the events
                var stateForActiveUser = gameStatePreparator.PrepareGameState(ActivePlayerSide, PassivePlayerSide, events);
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
                    var newEvents = userInteractionProcessor.ProcessInteraction(ActivePlayerSide, PassivePlayerSide, interaction);
                    events.AddRange(newEvents);
                    if (events.Any(x => x is GameEventPlayerDeath))
                    {
                        logger.Log(LogType.Arbiter, LogSeverity.Info, $"{ActivePlayerSide.Player.Name} Won");

                        // TODO: find a more approriate way to stop the game
                        return new GameResult()
                        {
                            IsOk = true,
                            IsFirstPlayerWon = isPlayerOneActive
                        };
                    }

                    stateForActiveUser = gameStatePreparator.PrepareGameState(ActivePlayerSide, PassivePlayerSide, events);
                    ActivePlayerInteractor.Update(new GameStateUpdate() { Events = events, NewState = stateForActiveUser });
                }

                //TODO: end of turn events here
                isPlayerOneActive = !isPlayerOneActive;
            }

            return null;
        }
    }
}
