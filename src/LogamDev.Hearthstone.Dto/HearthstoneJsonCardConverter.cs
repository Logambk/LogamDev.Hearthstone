using System.Collections.Generic;
using LogamDev.Hearthstone.Dto.HearthstoneJson;
using LogamDev.Hearthstone.Dto.Interface;
using LogamDev.Hearthstone.Vo.Card;

namespace LogamDev.Hearthstone.Dto
{
    public class HearthstoneJsonCardConverter : IHearthstoneJsonCardConverter
    {
        private readonly Dictionary<CardClass, Vo.Enum.CardClass> classMapping =
            new Dictionary<CardClass, Vo.Enum.CardClass>()
            {
                { CardClass.DRUID, Vo.Enum.CardClass.Druid },
                { CardClass.HUNTER, Vo.Enum.CardClass.Hunter },
                { CardClass.MAGE, Vo.Enum.CardClass.Mage },
                { CardClass.PALADIN, Vo.Enum.CardClass.Paladin },
                { CardClass.PRIEST, Vo.Enum.CardClass.Priest },
                { CardClass.ROGUE, Vo.Enum.CardClass.Rogue },
                { CardClass.SHAMAN, Vo.Enum.CardClass.Shaman },
                { CardClass.WARLOCK, Vo.Enum.CardClass.Warlock },
                { CardClass.WARRIOR, Vo.Enum.CardClass.Warrior },
                { CardClass.NEUTRAL, Vo.Enum.CardClass.Neutral }
            };

        private readonly Dictionary<CardType, Vo.Enum.CardType> typeMapping =
            new Dictionary<CardType, Vo.Enum.CardType>()
            {
                { CardType.SPELL, Vo.Enum.CardType.Spell },
                { CardType.MINION, Vo.Enum.CardType.Minion },
                { CardType.WEAPON, Vo.Enum.CardType.Weapon },
                { CardType.HERO, Vo.Enum.CardType.Hero }
            };

        private readonly Dictionary<Rarity, Vo.Enum.CardRarity> rarityMapping =
            new Dictionary<Rarity, Vo.Enum.CardRarity>()
            {
                { Rarity.FREE, Vo.Enum.CardRarity.Free },
                { Rarity.COMMON, Vo.Enum.CardRarity.Common },
                { Rarity.RARE, Vo.Enum.CardRarity.Rare },
                { Rarity.EPIC, Vo.Enum.CardRarity.Epic },
                { Rarity.LEGENDARY, Vo.Enum.CardRarity.Legendary }
            };

        public List<CardBase> Convert(List<Card> cards)
        {
            if (cards != null)
            {
                return cards.ConvertAll(Convert);
            }

            return null;
        }

        private CardBase Convert(Card cardDto)
        {
            CardBase card = null;
            if (cardDto != null && cardDto.Type != null && cardDto.Class != null && cardDto.DbfId != null
                && typeMapping.ContainsKey(cardDto.Type.Value)
                && classMapping.ContainsKey(cardDto.Class.Value)
                && rarityMapping.ContainsKey(cardDto.Rarity.Value))
            {
                switch (cardDto.Type)
                {
                    case CardType.SPELL:
                        card = new CardSpell();
                        break;

                    case CardType.MINION:
                        if (cardDto.Attack != null && cardDto.Health != null)
                        {
                            card = new CardMinion()
                            {
                                Attack = cardDto.Attack.Value,
                                Health = cardDto.Health.Value
                            };
                        }

                        break;

                    case CardType.WEAPON:
                        if (cardDto.Attack != null && cardDto.Durability != null)
                        {
                            card = new CardWeapon()
                            {
                                Attack = cardDto.Attack.Value,
                                Durability = cardDto.Durability.Value
                            };
                        }

                        break;

                    case CardType.HERO:
                        if (cardDto.Armor != null)
                        {
                            card = new CardHero()
                            {
                                Armor = cardDto.Armor.Value
                            };
                        }

                        break;
                }

                if (card != null)
                {
                    card.Cost = cardDto.Cost;
                    card.Type = typeMapping[cardDto.Type.Value];
                    card.Class = classMapping[cardDto.Class.Value];
                    card.Rarity = rarityMapping[cardDto.Rarity.Value];
                    card.DbfId = cardDto.DbfId.Value;
                }
            }

            return card;
        }
    }
}
