using System;
using System.Collections.Generic;
using System.Linq;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.Game;

namespace LogamDev.Hearthstone.Services.Conversion
{
    /// <summary>
    /// This Converter only converts dto to vo, it is not responsible for desck validation
    /// </summary>
    public class DeckConverter : IDeckConverter
    {
        private readonly ICardLibrary cardLibrary;

        public DeckConverter(ICardLibrary cardLibrary)
        {
            this.cardLibrary = cardLibrary;
        }

        public Deck Convert(Dto.Deck deckDto)
        {
            var deck = new Deck() { Cards = new Dictionary<CardBase, int>() };
            var isOk = true;
            if (Enum.TryParse<CardClass>(deckDto.Class, out var parsedClass))
            {
                deck.Class = parsedClass;
            }
            else
            {
                isOk = false;
            }

            if (Enum.TryParse<DeckFormat>(deckDto.Format, out var parsedFormat))
            {
                deck.Format = parsedFormat;
            }
            else
            {
                isOk = false;
            }

            if (!string.IsNullOrWhiteSpace(deckDto.Name))
            {
                deck.Name = deckDto.Name;
            }
            else
            {
                isOk = false;
            }

            foreach (var cardDto in deckDto.Cards)
            {
                var card = cardLibrary.CollectibleCards.FirstOrDefault(x => x.Name == cardDto.Key);
                if (card == null)
                {
                    isOk = false;
                    break;
                }

                deck.Cards.Add(card.Clone(), cardDto.Value);
            }

            if (isOk)
            {
                return deck;
            }

            return null;
        }
    }
}
