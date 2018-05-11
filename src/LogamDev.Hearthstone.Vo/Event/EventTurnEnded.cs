using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventTurnEnded : EventBase
    {
        public EventTurnEnded() : base(GameEventType.TurnEnded)
        {
        }
    }
}
