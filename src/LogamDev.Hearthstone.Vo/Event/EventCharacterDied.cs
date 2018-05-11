using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterDied : EventBase
    {
        public Guid DiedCharacter { get; set; }

        public EventCharacterDied() : base(GameEventType.CharacterDied)
        {
        }
    }
}
