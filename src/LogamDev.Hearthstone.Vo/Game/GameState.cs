using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.GameEvent;

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

        //TODO: re-think about how to handle temporary turn things like attacks
        public List<GameEventBase> ThisTurnEvents { get; set; }
    }
}
