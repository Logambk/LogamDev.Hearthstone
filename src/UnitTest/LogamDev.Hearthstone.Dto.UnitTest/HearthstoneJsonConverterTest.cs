using System.Linq;
using FluentAssertions;
using LogamDev.Hearthstone.Dto.Interface;
using LogamDev.Hearthstone.Dto.UnitTest.TestData;
using Newtonsoft.Json.Linq;
using Unity;
using Xunit;

namespace LogamDev.Hearthstone.Dto.UnitTest
{
    public class HearthstoneJsonConverterTest
    {
        [Theory]
        [ClassData(typeof(TestDataVersions))]
        public void ShouldConvertAllCollectibleCards(JArray collectible, JArray nonCollectible)
        {
            var parser = ContainerProvider.OriginalContainer.Resolve<IHearthstoneJsonCardParser>();
            var cardDtos = parser.Parse(collectible);

            // filter our default "hero skins" from playable hero cards
            var cardDtosFiltered = cardDtos.Where(x => !(x.Type == HearthstoneJson.CardType.HERO && x.IsElite != true)).ToList();

            var converter = ContainerProvider.OriginalContainer.Resolve<IHearthstoneJsonCardConverter>();
            var cardVos = converter.Convert(cardDtosFiltered);
            cardVos.Count().Should().Be(cardDtosFiltered.Count());
            cardVos.Should().NotContainNulls();
        }
    }
}
