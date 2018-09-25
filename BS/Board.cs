using System;
using System.Collections.Generic;
using System.Linq;

namespace BS
{
    public class Board
    {
        public const int MaxRow = 10;
        public const int MaxColumn = 10;

        public List<Ship> Ships { get; private set; }

        private bool[,] _coorodinates;

        public Board()
        {
            Ships = new List<Ship>();

            _coorodinates = new bool[MaxRow, MaxColumn];
        }

        public bool AddShip(Coordinates loc, Ship ship, Direction direction)
        {
            if (InvalidCapacity(ship))
            {
                return false;
            }

            var shipSize = (int)ship;

            var target = new Coordinates(0, 0);

            if (direction == Direction.Down)
            {
                target.X = loc.X;
                target.Y = loc.Y + (int)ship;

            }
            else
            {
                target.X = target.X + (int)ship; ;
                target.Y = loc.Y;

            }

            return SetShip(loc, target);

        }

        private bool InvalidCapacity(Ship ship)
        {
            if (ship == Ship.Battelship)
            {
                if (Ships.Count(s => s == Ship.Battelship) == 1)
                {
                    return true;
                }
            }
            else
            {
                if (Ships.Count(s => s == Ship.Destroyer) == 2)
                {
                    return false;
                }
            }
            return false;
        }


        private bool SetShip(Coordinates startLoc, Coordinates endLoc)
        {
            if (!ValidCoordinates(startLoc) || !ValidCoordinates(endLoc))
            {
                return false;
            }

            for (int x = startLoc.X; x < endLoc.X; x++)
            {
                if ((bool)_coorodinates.GetValue(x, startLoc.Y))
                {
                    return false;
                }
                _coorodinates.SetValue(true, x, startLoc.Y);
            }
            for (int y = startLoc.Y; y < endLoc.Y; y++)
            {
                if ((bool)_coorodinates.GetValue(startLoc.X, y))
                {
                    return false;
                }
                _coorodinates.SetValue(true, startLoc.X, y);
            }

            return true;
        }

        private bool ValidCoordinates(Coordinates loc)
        {
            if (loc.Y < 0 || loc.Y >= MaxColumn || loc.X < 0 || loc.X >= MaxRow)
            {
                return false;
            }
            return true;
        }
    }
}