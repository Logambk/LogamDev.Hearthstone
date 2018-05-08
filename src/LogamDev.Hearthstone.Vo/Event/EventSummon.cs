using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventSummon : EventBase
    {
        public Guid MinionId { get; set; }

        public EventSummon() : base(GameEventType.Summon)
        {
        }
    }
}
