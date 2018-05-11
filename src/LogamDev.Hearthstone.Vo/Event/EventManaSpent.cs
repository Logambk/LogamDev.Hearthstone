using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventManaSpent : EventBase
    {
        public ManaStorage ManaStorage { get; set; }
        public int ManaPoints { get; set; }

        public EventManaSpent(GameEventType type) : base(GameEventType.ManaSpent)
        {
        }
    }
}
