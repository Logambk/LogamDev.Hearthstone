using System;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.Interaction
{
    public class InteractionPlayCard : InteractionBase
    {
        public Guid CardId { get; set; }
        public Target Target { get; set; }
        public int? MinionPosition { get; set; }
    }
}
