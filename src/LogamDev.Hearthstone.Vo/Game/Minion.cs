using System;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Minion
    {
        public Minion(CardMinion cardMinion)
        {
            Card = cardMinion;
            Attack = cardMinion.Attack;
            Health = cardMinion.Health;
            Id = Guid.NewGuid();
        }

        public CardMinion Card { get; private set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public Guid Id { get; set; }
    }
}
