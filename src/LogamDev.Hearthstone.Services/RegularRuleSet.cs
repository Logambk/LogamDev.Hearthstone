using LogamDev.Hearthstone.Services.Interface;

namespace LogamDev.Hearthstone.Services
{
    public class RegularRuleSet : IRuleSet
    {
        public int DeckMaxNonLegendaryCards => 2;
        public int DeckMaxLegendaryCards => 1;
        public int DeckSize => 30;
        public int FieldMaxMinionsAtSide => 7;
        public int HandStartingSize => 3;
        public int HandMaxSize => 10;
        public int PlayerStartingHealth => 30;
        public int PlayerStartingManaCrystals => 0;
        public int PlayerMaxManaCrystals => 10;
    }
}
