using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Services
{
    public class UserInteractionProcessor : IUserInteractionProcessor
    {
        public List<GameEvent> ProcessInteraction(InternalSide internalSideActivePlayer, InternalSide opponentSide, UserInteractionBase userInteraction)
        {
            if (userInteraction is UserInteractionPlayCard)
            {
                return ProcessPlayCard(internalSideActivePlayer, opponentSide, userInteraction as UserInteractionPlayCard);
            }
            else if (userInteraction is UserInteractionAttack)
            {
                //TODO: support interaction
                return new List<GameEvent>();
            }
            else if (userInteraction is UserInteractionEndTurn)
            {
                //TODO: support interaction
                return new List<GameEvent>();
            }
            else
            {
                //TODO: handle unknown type
                return new List<GameEvent>();
            }
        }

        private List<GameEvent> ProcessPlayCard(InternalSide internalSideActivePlayer, InternalSide opponentSide, UserInteractionPlayCard userInteractionPlayCard)
        {
            var card = internalSideActivePlayer.Hand.First(x => x.Id == userInteractionPlayCard.CardId);
            //TODO: add mana service to handle mana payments
            var manaNeeded = card.Cost;
            if (internalSideActivePlayer.Player.TemporaryManaCrystals > 0)
            {
                var appliedMana = Math.Min(manaNeeded, internalSideActivePlayer.Player.TemporaryManaCrystals);
                manaNeeded -= appliedMana;
                internalSideActivePlayer.Player.TemporaryManaCrystals -= appliedMana;
            }

            if (manaNeeded > 0)
            {
                internalSideActivePlayer.Player.UsedPermanentManaCrystals += manaNeeded;
            }

            //TODO: handle opponent secrets somewhere here
            internalSideActivePlayer.Hand.Remove(card);

            switch (card.Type)
            {
                case CardType.Spell:
                    break;
                case CardType.Minion:
                    var minion = new Minion(card as CardMinion);
                    internalSideActivePlayer.Minions.Insert(userInteractionPlayCard.MinionPosition.Value, minion);
                    break;
                case CardType.Weapon:
                    break;
                case CardType.Hero:
                    break;
            }

            return new List<GameEvent>();
        }
    }
}
