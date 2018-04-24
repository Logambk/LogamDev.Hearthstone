using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogamDev.Hearthstone.Dto.Interface;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using Newtonsoft.Json.Linq;

namespace LogamDev.Hearthstone.Services
{
    public class CardLibrary : ICardLibrary
    {
        private const string CollectibleCardJsonPath = @"api\cards.collectible.json";
        private List<CardBase> cards = null;

        public CardLibrary(IHearthstoneJsonCardParser hearthstoneJsonCardParser, IHearthstoneJsonCardConverter hearthstoneJsonCardConverter)
        {
            var cardsJsonArray = JArray.Parse(File.ReadAllText(CollectibleCardJsonPath));
            var cardDtos = hearthstoneJsonCardParser.Parse(cardsJsonArray);
            var cardDtosFiltered = cardDtos.Where(x => !(x.Type == Dto.HearthstoneJson.CardType.HERO && x.IsElite != true)).ToList();
            cards = hearthstoneJsonCardConverter.Convert(cardDtosFiltered);
        }

        public List<CardBase> CollectibleCards => cards;
    }
}
