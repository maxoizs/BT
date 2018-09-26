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
        public void When_CreateBoard_ShouldSetCoordinates_ToAvailableMax()
        {
            var board = new Board();

            Assert.That(board.Coordinates.GetLength(0), Is.EqualTo(Board.MaxRow));
            Assert.That(board.Coordinates.GetLength(1), Is.EqualTo(Board.MaxColumn));
        }

        [Test]
        public void When_GenerateShips_ShouldCreateThreeShips()
        {
            var board = new Board();

            board.GenerateShips();

            var expected = new List<Ship> { Ship.Battelship, Ship.Destroyer, Ship.Destroyer };
            CollectionAssert.AreEquivalent(expected, board.Ships);
        }

        [Test]
        public void GiveCoordinatesAndShip_BoardShouldAddShip()
        {
            var board = new Board();

            var added = board.AddShip(Ship.Destroyer, new Coordinates(4, 3), Direction.Down);

            Assert.True(added);
            Assert.That(board.Ships, Has.Count.EqualTo(1));
            Assert.That(board.Ships.First(), Is.EqualTo(Ship.Destroyer));
        }

        [Test]
        public void GiveRepeatedCoordinatesAndShip_BoardShouldNotAddShip()
        {
            var board = new Board();

            board.AddShip(Ship.Destroyer, new Coordinates(4, 3), Direction.Down);
            var added = board.AddShip(Ship.Destroyer, new Coordinates(4, 3), Direction.Down);

            Assert.False(added);
        }

        [Test]
        public void GiveCrosedCoordinatesAndShip_BoardShouldNotAddShip()
        {
            var board = new Board();

            board.AddShip(Ship.Destroyer, new Coordinates(4, 3), Direction.Down);
            var added = board.AddShip(Ship.Destroyer, new Coordinates(4, 4), Direction.Down);

            Assert.False(added);
        }

        [Test]
        public void GiveInvalidCoordinatesAndShip_BoardShouldNotAddShip()
        {
            var board = new Board();
            
            var added = board.AddShip(Ship.Destroyer, new Coordinates(14, 13), Direction.Down);

            Assert.False(added);
        }

        [Test]
        public void GiveStringCoordinatesAndShip_BoardShouldNotAddShip()
        {
            var board = new Board();

            var added = board.AddShip(Ship.Destroyer,"A5", "D");           
            Assert.True(added);
            Assert.That(board.Coordinates[0, 5], Is.EqualTo(Cell.Shipe));
        }
    }
}
