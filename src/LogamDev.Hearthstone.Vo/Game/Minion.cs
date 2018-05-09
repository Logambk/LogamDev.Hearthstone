using System;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Minion
    {
        public Minion(CardMinion cardMinion, int orderNumber)
        {
            Attack = cardMinion.Attack;
            Card = cardMinion;
            OrderNumber = orderNumber;
            Health = cardMinion.Health;
            Id = Guid.NewGuid();
        }

        public int Attack { get; set; }
        public CardMinion Card { get; private set; }
        public int OrderNumber { get; private set; }
        public int Health { get; set; }
        public Guid Id { get; private set; }
    }
}
