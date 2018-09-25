using System;
using System.Collections.Generic;
using System.Linq;

namespace BS
{
    public class Board
    {
        private Random _rand = new Random();

        private const int ShipCapacity = 3;
        public const int MaxRow = 10;
        public const int MaxColumn = 10;

        private Dictionary<string, int> RowLabels = new Dictionary<string, int>{
            {"A",0},{"B",1},{"C",2},{"D",3},{"E",4},{"F",5},{"G",6},{"H",7},{"I",8}, {"J",9}};

        public List<Ship> Ships { get; private set; }

        private Cell[,] _coorodinates;
        private int _hits = 0;
        private const int MaxHits = (int)Ship.Battelship + (int)Ship.Destroyer + (int)Ship.Destroyer;
        private int _misses = 0;
        private const int MaxMisses = 100 - MaxHits;

        public Board()
        {
            Ships = new List<Ship>();

            _coorodinates = new Cell[MaxRow, MaxColumn];
        }

        public void GenerateShips()
        {
            while (!AddShip(Ship.Battelship)) ;
            while (!AddShip(Ship.Destroyer)) ;
            while (!AddShip(Ship.Destroyer)) ;
        }

        private bool AddShip(Ship ship)
        {
            var loc = new Coordinates(_rand.Next(Board.MaxRow), _rand.Next(Board.MaxColumn));

            return AddShip(ship, loc, (Direction)_rand.Next(1, 2));
        }

        public bool TakeHit(string loc)
        {
            if( _hits == MaxHits || _misses > MaxMisses){
                InvalidAction($"Hits:{_hits} and Misses:{_misses} you can't take No More ");
                return false; 
            }
            var coords = GetCoordinations(loc);
            if (_coorodinates[coords.X, coords.Y]==Cell.Shipe)
            {
                _coorodinates[coords.X, coords.Y] = Cell.Hit;
                _hits++;
                return true;
            }
             _coorodinates[coords.X, coords.Y] = Cell.Miss;
            _misses++;
            return false;
        }

        public bool IsLive()
        {
            return _hits<MaxHits; 
        }
        public bool AddShip(Ship ship, Coordinates loc, Direction direction)
        {
            if (InvalidCapacity(ship))
            {
                return false;
            }

            var shipSize = (int)ship;

            var x = 0;
            var y = 0;

            if (direction == Direction.Down)
            {
                x = loc.X;
                y = loc.Y + (int)ship;
            }
            else
            {
                x = loc.X + (int)ship; ;
                y = loc.Y;
            }

            var added = SetShip(loc, new Coordinates(x, y));
            if (!added)
            {
                Log.Output($"Cannot add {ship} on your board at {loc} toward {direction}");
            }
            else
            {
                Log.Output($"Ship {ship} added on your board at {loc} toward {direction}");
            }
            return added;
        }

        public bool AddShip(Ship ship, string loc, string direction)
        {
            var coords = GetCoordinations(loc);
            if (coords == null)
            {
                return false;
            }

            Direction dir;
            if (direction.ToUpper() == "D")
            {
                dir = Direction.Down;
            }
            else if (direction.ToUpper() == "R")
            {
                dir = Direction.Right;
            }
            else
            {
                InvalidAction($"Invalid direction: {direction} please chose [U]p or [D]own");
                return false;
            }

            return AddShip(ship, coords, dir);
        }

        private Coordinates GetCoordinations(string loc)
        {
            var x = 0;
            var y = 0;

            if (RowLabels.ContainsKey(loc[0].ToString().ToUpper()))
            {
                x = RowLabels[loc[0].ToString().ToUpper()];
            }
            else
            {
                InvalidAction("Row doesn't exists");
                return null;
            }


            if (!int.TryParse(loc[1].ToString(), out y))
            {
                InvalidAction("Column is not digit");
                return null;
            }

            return new Coordinates(x, y);
        }

        private void InvalidAction(string error)
        {
            Log.Output($"{error}, please check the correct value");
        }

        private bool InvalidCapacity(Ship ship)
        {
            if (Ships.Count == ShipCapacity)
            {
                Log.Output($"You have the maximum number of ships:({ShipCapacity})!");
                return true;
            }

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