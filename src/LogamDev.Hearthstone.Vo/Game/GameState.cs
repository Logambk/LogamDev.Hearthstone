using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class GameState
    {
        public Player You { get; set; }
        public Player Opponent { get; set; }
        public List<Minion> YourMinions { get; set; }
        public List<Minion> OpponentMinions { get; set; }
        public List<CardBase> Hand { get; set; }
        public int OpponentHandSize { get; set; }
        public int YourDeckSize { get; set; }
        public int OpponentDeckSize { get; set; }
    }
}
