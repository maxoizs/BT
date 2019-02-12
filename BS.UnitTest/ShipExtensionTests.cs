using NUnit.Framework;

namespace BS.UnitTest
{
    [TestFixture]
    public class ShipExtensionTests
    {
        public void GivenDestroyer_ShouldGetDestroyerCell()
        {
            Assert.That(new Destroyer().ToCell(), Is.EqualTo(Cell.Destroyer));
        }

        [Test]
        public void GivenBattelship_ShouldGetBattelshipCell()
        {
            Assert.That(new Battleship().ToCell(), Is.EqualTo(Cell.Battleship));
        }
    }
}