using LogamDev.Hearthstone.Vo.Interaction;
using LogamDev.Hearthstone.Vo.State;

namespace LogamDev.Hearthstone.Arbiter.Interface
{
    public interface IUserInteractor
    {
        InteractionBase Interact();
        void Update(GameStateUpdate gameStateUpdate);
    }
}
