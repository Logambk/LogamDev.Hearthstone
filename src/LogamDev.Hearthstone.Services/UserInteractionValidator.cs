using System;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Services
{
    public class UserInteractionValidator : IUserInteractionValidator
    {
        public ValidationResult ValidateUserInteraction(GameState currentState, UserInteractionBase interaction)
        {
            throw new NotImplementedException();
        }
    }
}
