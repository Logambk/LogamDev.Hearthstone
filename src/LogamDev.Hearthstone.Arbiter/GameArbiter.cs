using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Arbiter
{
    public class GameArbiter : IGameArbiter
    {
        private readonly IDeckValidator deckValidator;
        private readonly IGameStatePreparator gameStatePreparator;
        private readonly IRuleSet ruleSet;
        private readonly IUserInteractionProcessor userInteractionProcessor;
        
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
            IUserInteractionProcessor userInteractionProcessor)
        {
            this.deckValidator = deckValidator;
            this.gameStatePreparator = gameStatePreparator;
            this.ruleSet = ruleSet;
            this.userInteractionProcessor = userInteractionProcessor;
        }

        public GameResult StartGame(
            PlayerInitializer playerInitializer1,
            PlayerInitializer playerInitializer2,
            IUserInteractor playerInteractor1,
            IUserInteractor playerInteractor2)
        {
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
            isPlayerOneActive = true;
            var internalTurnNumber = 1;
            var turnNumberMax = 400;

            while (internalTurnNumber < turnNumberMax)
            {
                //TODO: turn structure here
                //TODO: server events here
                if (ActivePlayerSide.Player.TotalPermanentManaCrystals < ruleSet.PlayerMaxManaCrystals)
                {
                    ActivePlayerSide.Player.TotalPermanentManaCrystals++;
                }

                //TODO: draw the card from the deck
                //TODO: start of turn events here
                //TODO: update the state to both users
                //TODO: send the events
                var stateForActiveUser = gameStatePreparator.PrepareGameState(ActivePlayerSide, PassivePlayerSide);
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

                    if (interaction is UserInteractionEndTurn)
                    {
                        break;
                    }

                    //TODO: send the events to other user
                    var events = userInteractionProcessor.ProcessInteraction(ActivePlayerSide, PassivePlayerSide, interaction);
                    stateForActiveUser = gameStatePreparator.PrepareGameState(ActivePlayerSide, PassivePlayerSide);
                    ActivePlayerInteractor.Update(new GameStateUpdate() { Events = events, NewState = stateForActiveUser });
                }

                //TODO: end of turn events here
                isPlayerOneActive = !isPlayerOneActive;
            }

            return null;
        }
    }
}
