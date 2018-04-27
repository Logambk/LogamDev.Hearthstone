using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Interaction
{
    public abstract class InteractionBase
    {
        public InteractionType Type { get; private set; }

        public InteractionBase(InteractionType type)
        {
            Type = type;
        }
    }
}
