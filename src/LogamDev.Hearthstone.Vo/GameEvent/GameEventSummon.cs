using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.GameEvent
{
    public class GameEventSummon : GameEventBase
    {
        public Guid MinionId { get; set; }

        public GameEventSummon() : base(GameEventType.Summon)
        {
        }
    }
}
