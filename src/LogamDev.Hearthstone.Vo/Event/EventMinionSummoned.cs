using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventMinionSummoned : EventBase
    {
        public Guid MinionId { get; set; }

        public EventMinionSummoned() : base(GameEventType.MinionSummoned)
        {
        }
    }
}
