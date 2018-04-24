using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.GameEvent
{
    public class GameEventPlayerDeath : GameEventBase
    {
        public GameEventPlayerDeath() : base(GameEventType.PlayerDeath)
        {
        }
    }
}
