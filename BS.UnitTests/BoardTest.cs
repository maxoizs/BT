using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BS;

namespace BS.UnitTesting
{
    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void Add_Item()
        {
            var board = new Board();
            var added = board.AddShip( Ship.Destroyer, new Coordinates(4, 3),Direction.Down);
            Assert.That(board, Is.Not.Null);
        }

        [Test]
        public void Start()
        {
            var game = new Game();
            game.StartSinglePlayerGame("Mohammad");
            Assert.That(game, Is.Not.Null);
        }
    }
}
