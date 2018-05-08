using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterDealsDamage : EventBase
    {
        public Guid Damager { get; set; }
        public Guid Damaged { get; set; }
        public int DamagePoints { get; set; }

        public EventCharacterDealsDamage() : base(GameEventType.CharacterDealsDamage)
        {
        }
    }
}
