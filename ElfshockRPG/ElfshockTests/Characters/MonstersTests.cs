using ElfshockRPG.Characters;

namespace ElfshockTests.Characters
{
    [TestFixture]
    public class MonstersTests
    {
        // Verifies that a Monster is initialized with random stats
        // within the expected range

        [Test]
        public void Monster_Should_Initialize_With_Stats()
        {
            var monster = new Monster(0, 0);

            Assert.IsTrue(monster.Strength >= 1 && monster.Strength <= 3);
            Assert.IsTrue(monster.Agility >= 1 && monster.Agility <= 3);
            Assert.IsTrue(monster.Intelligence >= 1 && monster.Intelligence <= 3);
            Assert.AreEqual(1, monster.Range);
            Assert.AreEqual('0', monster.Symbol);
        }

        // Verifies that a Monster's position (X, Y) is set correctly

        [Test]
        public void Monster_Should_Have_Position()
        {
            var monster = new Monster(5, 7);

            Assert.AreEqual(5, monster.X);
            Assert.AreEqual(7, monster.Y);
        }
    }
}
