using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterAfterAttack : EventBase
    {
        public EventCharacterAfterAttack() : base(GameEventType.CharacterAfterAttack)
        {
        }
    }
}
