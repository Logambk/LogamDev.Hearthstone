using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterAttacks : EventBase
    {
        public Guid Attacker { get; set; }
        public Guid Attacked { get; set; }

        public EventCharacterAttacks() : base(GameEventType.CharacterAttacks)
        {
        }
    }
}
