using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventPlayerDeath : EventBase
    {
        public EventPlayerDeath() : base(GameEventType.PlayerDeath)
        {
        }
    }
}
