namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IRuleSet
    {
        int DeckMaxNonLegendaryCards { get; }
        int DeckMaxLegendaryCards { get; }
        int DeckSize { get; }
        int FieldMaxMinionsAtSide { get; }
        int HandStartingSize { get; }
        int HandMaxSize { get; }
        int PlayerStartingHealth { get; }
        int PlayerStartingManaCrystals { get; }
        int PlayerMaxManaCrystals { get; }
    }
}
