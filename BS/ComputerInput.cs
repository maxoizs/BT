using System;

namespace BS
{
    public class ComputerInput : IPlayerInput
    {
        private readonly Random _rand = new Random();

        public Coordinates GetCoordinates()
        {
            return new Coordinates(_rand.Next(Board.MaxRow), _rand.Next(Board.MaxColumn));
        }

        public Direction GetDirection()
        {
            return (Direction) _rand.Next(1, 3);
        }
    }
}