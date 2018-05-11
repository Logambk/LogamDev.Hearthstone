using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Vo.Event
{
    public abstract class EventBase
    {
        public GameEventType Type { get; private set; }
        public bool IsHiddenToOpponent { get; private set; }

        public EventBase(GameEventType type)
        {
            Type = type;
            IsHiddenToOpponent = false;
        }

        public EventFilter GetFilter()
        {
            return new EventFilter();
        }
    }
}
