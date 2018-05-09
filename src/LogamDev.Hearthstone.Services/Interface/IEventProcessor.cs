using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IEventProcessor
    {
        List<EventBase> ProcessEvent(FullGameState fullState, EventBase ev);
    }
}
