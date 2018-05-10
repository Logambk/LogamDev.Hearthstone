using System.Collections.Generic;

namespace LogamDev.Hearthstone.Dto
{
    public class Deck
    {
        public Dictionary<string, int> Cards { get; set; }
        public string Class { get; set; }
        public string Format { get; set; }
        public string Name { get; set; }
    }
}
