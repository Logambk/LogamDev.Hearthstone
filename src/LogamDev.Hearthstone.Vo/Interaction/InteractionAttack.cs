using System;

namespace LogamDev.Hearthstone.Vo.Interaction
{
    public class InteractionAttack : InteractionBase
    {
        public Guid? Attacker { get; set; }
        public Guid? Target { get; set; }
    }
}
