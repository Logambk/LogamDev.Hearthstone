using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterHealed : EventBase
    {
        public Guid Healed { get; set; }
        public int HealPoints { get; set; }

        public EventCharacterHealed() : base(GameEventType.CharacterHealed)
        {
        }
    }
}
