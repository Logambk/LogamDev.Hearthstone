using System.Linq;
using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Arbiter.UnitTest
{
    public class DummyMinionFacePlayer : IUserInteractor
    {
        private GameState gameState = null;

        public InteractionBase Interact()
        {
            // play minions
            var availableMinionsToPlay = gameState.Me.Hand.Where(x => x.Type == CardType.Minion && x.Cost <= gameState.Me.Mana.AvailableManaThisTurn).ToList();
            if (gameState.Me.Minions.Count < 7 && availableMinionsToPlay.Any())
            {
                var minionToPlay = availableMinionsToPlay.First();
                return new InteractionPlayCard()
                {
                    CardId = minionToPlay.Id,
                    MinionPosition = gameState.Me.Minions.Count,
                    ////Target = null
                };
            }

            // play artefact if you don't have one
            var artefactsToPlay = gameState.Me.Hand.Where(x => x.Type == CardType.Weapon && x.Cost <= gameState.Me.Mana.AvailableManaThisTurn).ToList();
            if (gameState.Me.Player.EquipedWeapon == null && artefactsToPlay.Any())
            {
                var artefactToPlay = artefactsToPlay.First();
                return new InteractionPlayCard()
                {
                    CardId = artefactToPlay.Id,
                    MinionPosition = null,
                    ////Target = null
                };
            }

            // go face with minions and with your weapon
            var thisTurnEvents = gameState.Me.Events.Last().Value;
            var attackersThisTurn = thisTurnEvents.Where(x => x.Type == GameEventType.CharacterAttacks).Select(x => (x as EventCharacterAttacks).Attacker).ToList();
            var minionIdsSummonedThisTurn = thisTurnEvents.Where(x => x.Type == GameEventType.Summon).Select(x => (x as EventSummon).MinionId).ToList();
            var yourMinionWhichCanAttack = gameState.Me.Minions.Where(x => !minionIdsSummonedThisTurn.Contains(x.Id) && !attackersThisTurn.Contains(x.Id)).ToList();
            if (yourMinionWhichCanAttack.Any())
            {
                return new InteractionAttack()
                {
                    Attacker = yourMinionWhichCanAttack.First().Id,
                    Target = gameState.Opp.Player.Id
                };
            }

            // and attack face yourself
            if (!attackersThisTurn.Contains(gameState.Me.Player.Id) && gameState.Me.Player.EquipedWeapon != null)
            {
                return new InteractionAttack()
                {
                    Attacker = gameState.Me.Player.Id,
                    Target = gameState.Opp.Player.Id
                };
            }

            // end turn
            return new InteractionEndTurn();
        }

        public void Update(GameState gameState)
        {
            this.gameState = gameState;
        }
    }
}
