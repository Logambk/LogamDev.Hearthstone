using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Event;

namespace LogamDev.Hearthstone.Vo.Utility
{
    public class PredicatedEvent
    {
        public EventBase Event { get; set; }
        public PredicateSide Side { get; set; }
        public EventFilter Filter { get; set; }
    }
}
