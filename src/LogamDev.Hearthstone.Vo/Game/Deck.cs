using System;
using System.Collections.Generic;
using LogamDev.Hearthstone.Vo.Card;
using LogamDev.Hearthstone.Vo.Enum;

namespace LogamDev.Hearthstone.Vo.Game
{
    public class Deck
    {
        public Dictionary<CardBase, int> Cards { get; set; }
        public CardClass Class { get; set; }
        public DeckFormat Format { get; set; }
        public string Name { get; set; }

        public List<CardBase> Init()
        {
            var cards = new List<CardBase>();
            foreach (var card in Cards)
            {
                for (int i = 0; i < card.Value; i++)
                {
                    var newCard = card.Key.Clone();
                    newCard.Id = Guid.NewGuid();
                    cards.Add(newCard);
                }
            }

            return cards;
        }
    }
}
