using System.Linq;
using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.GameEvent;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Arbiter.UnitTest
{
    public class DummyMinionFacePlayer : IUserInteractor
    {
        // plays only minions and always go face
        private GameState gameState = null;

        public InteractionBase Interact()
        {
            // play minions
            var availableMana = gameState.You.TotalPermanentManaCrystals - gameState.You.UsedPermanentManaCrystals;
            var availableMinionsToPlay = gameState.Hand.Where(x => x.Type == CardType.Minion && x.Cost <= availableMana).ToList();
            if (gameState.YourMinions.Count < 7 && availableMinionsToPlay.Any())
            {
                var minionToPlay = availableMinionsToPlay.First();
                return new InteractionPlayCard()
                {
                    CardId = minionToPlay.Id,
                    MinionPosition = gameState.YourMinions.Count,
                    Target = null
                };
            }

            var minionIdsSummonedThisTurn = gameState.ThisTurnEvents.Where(x => x.Type == GameEventType.Summon).Select(x => (x as GameEventSummon).MinionId).ToList();
            var minonsIdsWhoAtackedThisTurn = gameState.ThisTurnEvents.Where(x => x.Type == GameEventType.Attack).Select(x => (x as GameEventAttack).Attacker).ToList();
            var yourMinionWhichCanAttack = gameState.YourMinions.Where(x => !minionIdsSummonedThisTurn.Contains(x.Id) && !minonsIdsWhoAtackedThisTurn.Contains(x.Id)).ToList();
            if (yourMinionWhichCanAttack.Any())
            {
                return new InteractionAttack()
                {
                    Attacker = yourMinionWhichCanAttack.First().Id,
                    Target = null   //TODO: think about how to handle face attack targets
                };
            }

            return new InteractionEndTurn();
        }

        public void Update(GameStateUpdate gameStateUpdate)
        {
            gameState = gameStateUpdate.NewState;
        }
    }
}
