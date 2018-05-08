using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventEndTurn : EventBase
    {
        public EventEndTurn() : base(GameEventType.EndOfTurn)
        {
        }
    }
}
