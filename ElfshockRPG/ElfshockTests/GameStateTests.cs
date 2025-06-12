using ElfshockRPG.Enums;
using ElfshockRPG;

namespace ElfshockTests
{
    [TestFixture]
    public class GameStateTests
    {
        // Verifies that the game state initializes with default values

        [Test]
        public void GameState_Should_Initialize_With_DefaultValues()
        {
            Assert.AreEqual(GameScreen.MainMenu, GameState.CurrentScreen);
            Assert.AreEqual(1, GameState.PlayerX);
            Assert.AreEqual(1, GameState.PlayerY);
            Assert.AreEqual(0, GameState.Monsters.Count);
        }
    }
}
