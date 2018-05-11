using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCardPlayed : EventBase
    {
        public CardBase Card { get; set; }
        public int? MinionPosition { get; set; }
        ////public Target Target { get; set; }

        public EventCardPlayed() : base(GameEventType.CardPlayed)
        {
        }
    }
}
