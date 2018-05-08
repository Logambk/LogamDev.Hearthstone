using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Vo.Utility;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class TriggerStorage
    {
        private List<Trigger> triggers;

        public TriggerStorage()
        {
            triggers = new List<Trigger>();
        }

        public void Add(Trigger trigger)
        {
            triggers.Add(trigger);
        }

        public void RemoveBySourceId(Guid sourceId)
        {
            triggers.RemoveAll(x => x.SourceId == sourceId);
        }

        public List<PredicatedEvent> GetAssociatedActions(PredicatedEvent condition)
        {
            var mappedTriggers = new List<Trigger>();
            foreach (var trigger in triggers)
            {
                if (condition.Event.Type == trigger.Condition.Event.Type)
                {
                    // TODO: apply more generic condition checks
                    if (condition.Filter.ChracterId == trigger.Condition.Filter.ChracterId)
                    {
                        mappedTriggers.Add(trigger);
                    }
                }
            }

            var predicatedEvents = mappedTriggers.Select(x => x.Action).ToList();
            triggers.RemoveAll(x => x.IsOneTimeTrigger == true && mappedTriggers.Contains(x));

            return predicatedEvents;
        }
    }
}
