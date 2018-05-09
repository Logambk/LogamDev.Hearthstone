using LogamDev.Hearthstone.Vo.Enum;

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
    }
}
