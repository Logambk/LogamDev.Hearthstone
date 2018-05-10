using System.Collections.Generic;
using LogamDev.Hearthstone.Dto.HearthstoneJson;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Services.Interface
{
    public interface IHearthstoneJsonCardConverter
    {
        List<CardBase> Convert(List<Card> cards);
    }
}
