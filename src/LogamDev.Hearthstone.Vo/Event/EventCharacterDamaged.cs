using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterDamaged : EventBase
    {
        public Guid Damager { get; set; }
        public Guid Damaged { get; set; }

        public EventCharacterDamaged() : base(GameEventType.CharacterDamaged)
        {
        }
    }
}
