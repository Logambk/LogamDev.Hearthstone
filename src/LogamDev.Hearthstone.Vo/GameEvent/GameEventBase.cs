using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.GameEvent
{
    public abstract class GameEventBase
    {
        public GameEventType Type { get; set; }

        public GameEventBase(GameEventType type)
        {
            Type = type;
        }
    }
}
