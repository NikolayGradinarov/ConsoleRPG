using NUnit.Framework;
using ElfshockRPG.Characters;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace ElfshockTests.Characters
{
    [TestFixture]
    public class CharacterTests
    {
        // Verifies that the Warrior Class initializes with correct default values

        [Test]
        public void Warrior_Initilalization_IsCorrect()
        {
            var warrior = new Warrior();
            warrior.Setup();

            Assert.AreEqual(3, warrior.Strength);
            Assert.AreEqual(3, warrior.Agility);
            Assert.AreEqual(0, warrior.Intelligence);
            Assert.AreEqual(1, warrior.Range);
            Assert.AreEqual('@', warrior.Symbol);

            Assert.AreEqual(15, warrior.Health);        
            Assert.AreEqual(0, warrior.Mana);  
            Assert.AreEqual(6, warrior.Damage);
        }

        // Verifies that the Archer Class initializes with correct default values

        [Test]
        public void Archer_Initialization_IsCorrect()
        {
            var archer = new Archer();
            archer.Setup();

            Assert.AreEqual(2, archer.Strength);
            Assert.AreEqual(4, archer.Agility);
            Assert.AreEqual(0, archer.Intelligence);
            Assert.AreEqual(2, archer.Range);
            Assert.AreEqual('#', archer.Symbol);

            Assert.AreEqual(10, archer.Health);
            Assert.AreEqual(0, archer.Mana);
            Assert.AreEqual(8, archer.Damage);
        }

        // Verifies that the Mage Class initializes with correct default values

        [Test]
        public void Mage_Initialization_IsCorrect()
        {
            var mage = new Mage();
            mage.Setup();

            Assert.AreEqual(2, mage.Strength);
            Assert.AreEqual(1, mage.Agility);
            Assert.AreEqual(3, mage.Intelligence);
            Assert.AreEqual(3, mage.Range);
            Assert.AreEqual('*', mage.Symbol);

            Assert.AreEqual(10, mage.Health);
            Assert.AreEqual(9, mage.Mana);
            Assert.AreEqual(2, mage.Damage);
        }
    }
}
