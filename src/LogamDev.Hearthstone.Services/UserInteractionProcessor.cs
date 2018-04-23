using System.Collections.Generic;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Game;
using LogamDev.Hearthstone.Vo.Interaction;

namespace LogamDev.Hearthstone.Services
{
    public class UserInteractionProcessor : IUserInteractionProcessor
    {
        public List<GameEvent> ProcessInteraction(InternalSide internalSideActivePlayer, InternalSide opponentSide, UserInteractionBase userInteraction)
        {
            throw new System.NotImplementedException();
        }
    }
}
