using System.Linq;
using FluentAssertions;
using LogamDev.Hearthstone.Dto.Interface;
using LogamDev.Hearthstone.Dto.UnitTest.TestData;
using Newtonsoft.Json.Linq;
using Xunit;

namespace LogamDev.Hearthstone.Dto.UnitTest
{
    public class DeckPlainTextParserTest
    {
        [Fact]
        public void ShouldParseDeck1()
        {
            var parser = ContainerResolver.Resolve<IDeckPlainTextParser>();
            var cards = parser.ParseDeck(TestDataProvider.GetStringContent($@"TestData\Decks\Deck1.txt"));
            cards.Count.Should().Be(30);
        }

        [Theory]
        [ClassData(typeof(TestDataVersions))]
        public void ShouldFindAllCardsInCollectibles(JArray collectible, JArray nonCollectible)
        {
            var parser = ContainerResolver.Resolve<IHearthstoneJsonCardParser>();
            var cardDtos = parser.Parse(collectible);

            var deckParser = ContainerResolver.Resolve<IDeckPlainTextParser>();
            var cardNames = deckParser.ParseDeck(TestDataProvider.GetStringContent($@"TestData\Decks\Deck1.txt"));

            var mappedCards = cardNames.Select(x => cardDtos.FirstOrDefault(y => y.Name == x));
            mappedCards.Should().NotContainNulls();
        }
    }
}
