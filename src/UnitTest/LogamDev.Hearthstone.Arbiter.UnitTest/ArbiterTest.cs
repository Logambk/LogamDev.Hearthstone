using System.IO;
using FluentAssertions;
using LogamDev.Hearthstone.Arbiter.Interface;
using LogamDev.Hearthstone.Dto.Interface;
using LogamDev.Hearthstone.Services.Interface;
using LogamDev.Hearthstone.Vo.Enum;
using LogamDev.Hearthstone.Vo.State;
using Unity;
using Xunit;

namespace LogamDev.Hearthstone.Arbiter.UnitTest
{
    public class ArbiterTest
    {
        public string Deck1Path = @"TestData\Decks\Deck1.txt";

        [Fact]
        public void ShouldHandleBattleOfTwoRandomMinionFacePlayers()
        {
            var container = new UnityContainer();
            Dto.UnityConfig.Register(container);
            UnityConfig.Register(container);
            Services.UnityConfig.Register(container);

            var deckParser = container.Resolve<IDeckPlainTextParser>();
            var deckConverter = container.Resolve<IDeckConverter>();
            var deckDto = deckParser.ParseDeck(File.ReadAllText(Deck1Path));
            var deck = deckConverter.Convert(deckDto);

            var playerInitializer1 = new PlayerInitializer()
            {
                Class = CardClass.Hunter,
                Deck = deck,
                Name = "Player 1"
            };

            var playerInitializer2 = new PlayerInitializer()
            {
                Class = CardClass.Hunter,
                Deck = deck,
                Name = "Player 2"
            };

            var arbiter = container.Resolve<IGameArbiter>();
            var gameResult = arbiter.StartGame(playerInitializer1, playerInitializer2, new DummyMinionFacePlayer(), new DummyMinionFacePlayer());
            gameResult.IsOk.Should().Be(true);
        }
    }
}
