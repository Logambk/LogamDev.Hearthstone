using System.Linq;
using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Arbiter.UnitTest
{
    public class RandomMinionFacePlayer : IUserInteractor
    {
        // plays only minions and always go face
        private GameState gameState = null;

        public UserInteractionBase Interact()
        {
            var availableMana = gameState.You.TotalPermanentManaCrystals - gameState.You.UsedPermanentManaCrystals;
            var availableMinionsToPlay = gameState.Hand.Where(x => x.Type == CardType.Minion && x.Cost <= availableMana);
            if (gameState.YourMinions.Count < 7 && availableMinionsToPlay.Any())
            {
                var minionToPlay = availableMinionsToPlay.First();
                return new UserInteractionPlayCard()
                {
                    CardId = minionToPlay.Id,
                    MinionPosition = gameState.YourMinions.Count,
                    Target = null
                };
            }
            else
            {
                return new UserInteractionEndTurn();
            }
        }

        public void Update(GameStateUpdate gameStateUpdate)
        {
            gameState = gameStateUpdate.NewState;
        }
    }
}
