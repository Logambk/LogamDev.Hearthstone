using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Event
{
    public class EventProcessor : IEventProcessor
    {
        public List<EventBase> ProcessEvent(ServerGameState fullState, EventBase ev)
        {
            // TODO: Process the event
            return new List<EventBase> { ev };
        }
    }
}
