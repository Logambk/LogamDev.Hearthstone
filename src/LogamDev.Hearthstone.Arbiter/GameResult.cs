using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Arbiter
{
    public class GameResult
    {
        public bool IsOk { get; set; }
        public bool IsFirstPlayerWon { get; set; }
        public ServerGameState FinalState { get; set; }
    }
}
