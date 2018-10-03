using System;
using BS;
using Moq;
using NUnit.Framework;

namespace Tests
{

    [TestFixture]
    public class GameTests
    {
        private Mock<IDisplayBoard> _boardDisplayer;
        private Mock<IUserInput> _userInput;
        private Random _rand = new Random();

        [OneTimeSetUp]
        public void Setup()
        {
            _boardDisplayer = new Mock<IDisplayBoard>();
            _boardDisplayer.Setup(x => x.DisplayBoard(It.IsAny<IBoard>()))
            .Callback((IBoard b) => DisplayCallBack(b));

            _userInput = new Mock<IUserInput>();
            _userInput.Setup(x => x.GetDirection()).Returns(GenerateRandomDirection);
            _userInput.Setup(x => x.GetCoordinates()).Returns(GenerateRandomCoords);

        }

        private Coordinates GenerateRandomCoords()
        {
            return new Coordinates(_rand.Next(Board.MaxRow), _rand.Next(Board.MaxColumn));
        }

        private Coordinates GenerateLimitedCoords()
        {
            return new Coordinates(_rand.Next(4), _rand.Next(4));
        }

        private Direction GenerateRandomDirection()
        {
            return (Direction)_rand.Next(1, 3);
        }

        private void DisplayCallBack(IBoard board)
        {
        }

        [Test]
        public void GivenStartGame_PlayerName_ShouldAssignItToPlayer()
        {
            var game = new Game(_boardDisplayer.Object, _userInput.Object,_userInput.Object);
            var playerName = "Mohammad";

            game.Start(playerName);

            Assert.That(game.Player.Name, Is.EqualTo(playerName));
        }

        [Test]
        public void WhenGameStart_ShouldStopOnlyIfPlayerWin()
        {
            var game = new Game(_boardDisplayer.Object, _userInput.Object,_userInput.Object);
            var playerName = "Mohammad";

            game.Start(playerName);

            Assert.That(game.Winner, Is.Not.Null);
        }

        // [Test]
        public void GivenBadHits_ComputerShouldWin()
        {
            var userInput = new Mock<IUserInput>();
            userInput.Setup(x => x.GetDirection()).Returns(Direction.Down);
            // a Hack to limit hits 
            //( still there is a probability of having the computer's ship within this range)
            userInput.Setup(x => x.GetCoordinates()).Returns(GenerateLimitedCoords);
            var game = new Game(_boardDisplayer.Object, userInput.Object,_userInput.Object);
            var playerName = "Mohammad";

            game.Start(playerName);

            Assert.That(game.Winner, Is.Not.Null);
            Assert.That(game.Winner.Name, Is.EqualTo(game.Computer.Name));
        }
    }
}