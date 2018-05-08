using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventPlayCard : EventBase
    {
        public int Dbfid { get; set; }
        ////public Target Target { get; set; }

        public EventPlayCard() : base(GameEventType.PlayCard)
        {
        }
    }
}
