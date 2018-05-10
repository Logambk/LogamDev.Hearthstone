using System.IO;
using System.Linq;
using FluentAssertions;
using LogamDev.Hearthstone.Dto.HearthstoneJson;
using LogamDev.Hearthstone.Dto.Interface;
using LogamDev.Hearthstone.Services.Interface;
using Newtonsoft.Json.Linq;
using Unity;

namespace LogamDev.Hearthstone.Services.UnitTest
{
    public class HearthstoneJsonConverterTest
    {
        private const string CollectibleCardJsonPath = @"api\cards.collectible.json";

        public void ShouldConvertAllCollectibleCards()
        {
            var container = new UnityContainer();
            Dto.UnityConfig.Register(container);
            UnityConfig.Register(container);

            var collectible = JArray.Parse(File.ReadAllText(CollectibleCardJsonPath));
            var parser = container.Resolve<IHearthstoneJsonCardParser>();
            var cardDtos = parser.Parse(collectible);

            // filter our default "hero skins" from playable hero cards
            var cardDtosFiltered = cardDtos.Where(x => !(x.Type == CardType.HERO && x.IsElite != true)).ToList();

            var converter = container.Resolve<IHearthstoneJsonCardConverter>();
            var cardVos = converter.Convert(cardDtosFiltered);
            cardVos.Count().Should().Be(cardDtosFiltered.Count());
            cardVos.Should().NotContainNulls();
        }
    }
}
