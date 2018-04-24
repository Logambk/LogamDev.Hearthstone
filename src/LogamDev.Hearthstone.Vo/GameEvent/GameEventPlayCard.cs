using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.GameEvent
{
    public class GameEventPlayCard : GameEventBase
    {
        public int Dbfid { get; set; }
        public Target Target { get; set; }

        public GameEventPlayCard() : base(GameEventType.PlayCard)
        {
        }
    }
}
