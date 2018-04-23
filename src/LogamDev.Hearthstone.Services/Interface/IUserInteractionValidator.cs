using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IUserInteractionValidator
    {
        ValidationResult ValidateUserInteraction(GameState currentState, UserInteractionBase interaction);
    }
}
