using System;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Minion
    {
        public CardMinion Card { get; }

        public int Attack { get; set; }
        public int Health { get; set; }
        public Guid Id { get; set; }
    }
}
