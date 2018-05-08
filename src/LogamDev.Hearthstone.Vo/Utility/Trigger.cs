using System;

namespace LogamDev.Hearthstone.Vo.Utility
{
    public class Trigger
    {
        public bool IsOneTimeTrigger { get; set; }
        public Guid SourceId { get; set; }
        public PredicatedEvent Condition { get; set; }
        public PredicatedEvent Action { get; set; }
    }
}
