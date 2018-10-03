using BS;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class BoardTest
    {
        private IUserInput _userInput = new UserInput();

        [Test]
        public void When_GenerateShips_ShouldCreateThreeShips()
        {
            var board = new Board(_userInput);

            board.GenerateShips();

            var expected = new List<Ship> { new Battleship(), new Destroyer(), new Destroyer() };
            CollectionAssert.AreEquivalent(expected.Select(s => s.Name), board.Ships.Select(s => s.Name));
        }

        [Test]
        public void GiveCoordinatesAndShip_BoardShouldAddShip()
        {
            var board = new Board(_userInput);

            var added = board.AddShip(new Destroyer(), new Coordinates(4, 3), Direction.Down);

            Assert.True(added);
            Assert.That(board.Ships, Has.Count.EqualTo(1));
            Assert.That(board.Ships.First().Name, Is.EqualTo(new Destroyer().Name));
        }

        [Test]
        public void GiveRepeatedCoordinatesAndShip_BoardShouldNotAddShip()
        {
            var board = new Board(_userInput);

            board.AddShip(new Destroyer(), new Coordinates(4, 3), Direction.Down);
            var added = board.AddShip(new Destroyer(), new Coordinates(4, 3), Direction.Down);

            Assert.False(added);
        }

        [Test]
        public void GiveCrossedCoordinatesAndShip_BoardShouldNotAddShip()
        {
            var board = new Board(_userInput);

            board.AddShip(new Destroyer(), new Coordinates(4, 3), Direction.Down);
            var added = board.AddShip(new Destroyer(), new Coordinates(4, 4), Direction.Down);

            Assert.False(added);
        }

        [Test]
        public void GiveInvalidCoordinatesAndShip_BoardShouldNotAddShip()
        {
            var board = new Board(_userInput);

            var added = board.AddShip(new Destroyer(), new Coordinates(14, 13), Direction.Down);

            Assert.False(added);
        }

        [Test]
        public void GivenInValidHit_BoardShouldIncreaseMisses()
        {
            var board = new Board(_userInput);
            var added = board.AddShip(new Destroyer(), new Coordinates(1, 1), Direction.Down);

            board.TakeHit(new Coordinates(3, 3));

            Assert.That(board.Misses, Is.EqualTo(1));
            Assert.That(board.Hits, Is.EqualTo(0));
        }


        [Test]
        public void GivenValidHit_BoardShouldIncreaseHits()
        {
            var board = new Board(_userInput);
            var added = board.AddShip(new Destroyer(), new Coordinates(1, 1), Direction.Down);

            board.TakeHit(new Coordinates(1, 1));

            Assert.That(board.Misses, Is.EqualTo(0));
            Assert.That(board.Hits, Is.EqualTo(1));
        }


        [Test]
        public void GivenValidHitsEqualToShip_BoardLive_ShouldBeFalse()
        {
            var board = new Board(_userInput);
            var added = board.AddShip(new Destroyer(), new Coordinates(1, 1), Direction.Down);

            board.TakeHit(new Coordinates(1, 1));
            board.TakeHit(new Coordinates(1, 2));

            Assert.That(board.Hits, Is.EqualTo(2));
            Assert.IsFalse(board.IsLive());
        }

        [Test]
        public void GivenValidHitsTwice_BoardCell_ShouldShowHit()
        {
            var board = new Board(_userInput);
            var added = board.AddShip(new Destroyer(), new Coordinates(1, 1), Direction.Down);

            board.TakeHit(new Coordinates(1, 1));
            board.TakeHit(new Coordinates(1, 1));

            Assert.That(board.Hits, Is.EqualTo(1));
            Assert.That(board.Coordinates[1, 1], Is.EqualTo(Cell.Hit));
        }
    }
}