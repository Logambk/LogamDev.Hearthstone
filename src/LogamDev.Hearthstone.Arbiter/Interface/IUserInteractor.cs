using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Arbiter.Interface
{
    public interface IUserInteractor
    {
        UserInteractionBase Interact();
        void Update(GameStateUpdate gameStateUpdate);
    }
}
