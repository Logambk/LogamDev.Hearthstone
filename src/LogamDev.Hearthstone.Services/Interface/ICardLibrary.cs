using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface ICardLibrary
    {
        List<CardBase> CollectibleCards { get; }
    }
}
