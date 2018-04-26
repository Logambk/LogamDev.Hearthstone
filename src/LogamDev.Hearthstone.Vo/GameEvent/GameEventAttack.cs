using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.GameEvent
{
    public class GameEventAttack : GameEventBase
    {
        public Guid? Attacker { get; set; }
        public Guid? Target { get; set; }

        public GameEventAttack() : base(GameEventType.Attack)
        {
        }
    }
}
