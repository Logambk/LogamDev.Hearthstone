using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.GameEvent
{
    public class GameEventEndTurn : GameEventBase
    {
        public GameEventEndTurn() : base(GameEventType.EndOfTurn)
        {
        }
    }
}
