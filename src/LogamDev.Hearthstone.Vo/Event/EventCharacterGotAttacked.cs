using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterGotAttacked : EventBase
    {
        public Guid Attacker { get; set; }
        public Guid Attacked { get; set; }

        public EventCharacterGotAttacked() : base(GameEventType.CharacterGotAttacked)
        {
        }
    }
}
