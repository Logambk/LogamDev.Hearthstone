using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Vo.State
{
    public class PlayerInitializer
    {
        public string Name { get; set; }
        public Deck Deck { get; set; }
        public CardClass Class { get; set; }
    }
}
