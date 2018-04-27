using System;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Interaction
{
    public class InteractionAttack : InteractionBase
    {
        public Guid? Attacker { get; set; }
        public Guid? Target { get; set; }

        public InteractionAttack() : base(InteractionType.Attack)
        {
        }
    }
}
