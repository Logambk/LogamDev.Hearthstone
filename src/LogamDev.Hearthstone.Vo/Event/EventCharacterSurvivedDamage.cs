using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Event
{
    public class EventCharacterSurvivedDamage : EventBase
    {
        public EventCharacterSurvivedDamage() : base(GameEventType.CharacterSurvivedDamage)
        {
        }
    }
}
