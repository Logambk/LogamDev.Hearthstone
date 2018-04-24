using System.Linq;
using FluentAssertions;
using LogamDev.Hearthstone.Dto.HearthstoneJson;
using LogamDev.Hearthstone.Dto.Interface;
using LogamDev.Hearthstone.Dto.UnitTest.TestData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity;
using Xunit;

namespace LogamDev.Hearthstone.Dto.UnitTest
{
    public class HearthstoneJsonParserTest
    {
        private readonly string[] cardProperties = typeof(Card).GetProperties()
                 .SelectMany(p => p.GetCustomAttributes(typeof(JsonPropertyAttribute), false).Cast<JsonPropertyAttribute>())
                 .Select(jp => jp.PropertyName)
                 .ToArray();

        [Theory]
        [ClassData(typeof(TestDataVersions))]
        public void ShouldParseCardsWithoutException(JArray collectible, JArray nonCollectible)
        {
            var parser = ContainerResolver.Resolve<IHearthstoneJsonCardParser>();
            parser.Parse(collectible);
            parser.Parse(nonCollectible);
        }

        [Theory]
        [ClassData(typeof(TestDataVersions))]
        public void ShouldCoverAllJsonProperties(JArray collectible, JArray nonCollectible)
        {
            var unitedArray = new JArray(collectible.Union(nonCollectible));
            foreach (JObject card in unitedArray)
            {
                card.Properties().Should().OnlyContain(x => cardProperties.Contains(x.Name));
            }
        }

        [Theory]
        [ClassData(typeof(TestDataVersions))]
        public void ShouldFindAllEnumValues(JArray collectible, JArray nonCollectible)
        {
            var parser = ContainerResolver.Resolve<IHearthstoneJsonCardParser>();
            var collectibleCards = parser.Parse(collectible);
            var nonCollectibleCards = parser.Parse(nonCollectible);

            foreach (var card in collectibleCards.Union(nonCollectibleCards))
            {
                if (card.Class != null)
                {
                    card.Class.Should().NotBe(CardClass.INVALID);
                }
                
                if (card.Classes != null)
                {
                    card.Classes.Should().NotContain(CardClass.INVALID);
                }

                if (card.Faction != null)
                {
                    card.Faction.Should().NotBe(Faction.INVALID);
                }

                if (card.MultiClassGroup != null)
                {
                    card.MultiClassGroup.Should().NotBe(MultiClassGroup.INVALID);
                }

                if (card.Requirements != null)
                {
                    card.Requirements.Should().NotContainKey(PlayReq.INVALID);
                }

                if (card.Race != null)
                {
                    card.Race.Should().NotBe(Race.INVALID);
                }

                if (card.Rarity.HasValue)
                {
                    card.Rarity.Should().NotBe(Rarity.INVALID);
                }

                if (card.Set.HasValue)
                {
                    card.Set.Should().NotBe(CardSet.INVALID);
                }

                if (card.Type.HasValue)
                {
                    card.Type.Should().NotBe(CardType.INVALID);
                }
            }
        }
    }
}
