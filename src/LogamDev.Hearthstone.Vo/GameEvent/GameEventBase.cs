using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.GameEvent
{
    public abstract class GameEventBase
    {
        public GameEventType Type { get; private set; }

        public GameEventBase(GameEventType type)
        {
            Type = type;
        }
    }
}
