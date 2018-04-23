using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Services
{
    public class GameStatePreparator : IGameStatePreparator
    {
        public GameState PrepareGameState(InternalSide activeUser, InternalSide opponent)
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
                YourMinions = activeUser.Minions
            };
        }
    }
}
