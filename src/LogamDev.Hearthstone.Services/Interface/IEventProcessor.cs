using LogamDev.Hearthstone.Vo.Event;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IEventProcessor
    {
        void ProcessEvent(ServerGameState fullState, EventBase ev);
    }
}
