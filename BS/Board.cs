using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BS
{
    public class Board
    {
        private Random _rand = new Random();
        private const int ShipCapacity = 3;
        public const int MaxRow = 10;
        public const int MaxColumn = 10;
        public static readonly Dictionary<string, int> RowLabels = new Dictionary<string, int>{
            {"A",0},{"B",1},{"C",2},{"D",3},{"E",4},{"F",5},{"G",6},{"H",7},{"I",8}, {"J",9}};

        public List<Ship> Ships { get; private set; }
        public Cell[,] Coordinates { get; private set; }

        private int _hits = 0;
        private int _misses = 0;

        public Board()
        {
            _hits = 0;
            _misses = 0;
            Ships = new List<Ship>();
            Coordinates = GenerateBoardCells();

        }

        private Cell[,] GenerateBoardCells()
        {
            var cells = new Cell[MaxRow, MaxColumn]; ;
            for (var x = 0; x < MaxRow; x++)
                for (var y = 0; y < MaxColumn; y++)
                {
                    cells.SetValue(Cell.Empty, x, y);
                }
            return cells;
        }

        public void GenerateShips()
        {
            while (!AddShip(new Battleship())) ;
            while (!AddShip(new Destroyer())) ;
            while (!AddShip(new Destroyer())) ;
        }

        private bool AddShip(Ship ship)
        {
            var loc = new Coordinates(_rand.Next(Board.MaxRow), _rand.Next(Board.MaxColumn));

            return AddShip(ship, loc, (Direction)_rand.Next(1, 2));
        }

        public bool? TakeHit(Coordinates loc)
        {
            if (!IsLive())
            {
                InvalidAction($"All ships have been sunk, and player lost already!");
                return false;
            }            

            if (Coordinates[loc.X, loc.Y] == Cell.Destroyer || Coordinates[loc.X, loc.Y] == Cell.Battleship)
            {
                Coordinates[loc.X, loc.Y] = Cell.Hit;
                _hits++;
                return true;
            }

            Coordinates[loc.X, loc.Y] = Cell.Miss;
            _misses++;
            return false;
        }

        public bool IsLive()
        {
            return _hits >= (int)ShipType.Destroyer + (int)ShipType.Destroyer + (int)ShipType.Destroyer;
        }
        public bool AddShip(Ship ship, Coordinates loc, Direction direction)
        {
            if (InvalidCapacity(ship))
            {
                return false;
            }

            var targetLoc = CalculateCoords(ship.Size, loc, direction);

            var cells = GetEmptyCells(loc, targetLoc);

            if (cells == null)
            {
                Log.Output($"Cannot add {ship.Name} on your board at {loc} toward {direction}");
                return false;
            }

            UpdateCells(cells, ship);
            Ships.Add(ship);
            Log.Output($"Ship {ship} added on your board at {loc} toward {direction}");
            return true;
        }

        private void UpdateCells(List<Coordinates> coordinates, Ship ship)
        {
            foreach (var loc in coordinates)
            {
                Coordinates[loc.X, loc.Y] = ship.ToCell();
            }
        }

        private Coordinates CalculateCoords(int shipSize, Coordinates loc, Direction direction)
        {
            var x = 0;
            var y = 0;

            if (direction == Direction.Down)
            {
                x = loc.X;
                y = loc.Y + shipSize;
            }
            else
            {
                x = loc.X + shipSize;
                y = loc.Y;
            }
            return new Coordinates(x, y);
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

        public static Coordinates GetCoordinations(string loc)
        {
            var x = -1;
            var y = -1;

            if (RowLabels.Keys.Contains(loc[0].ToString().ToUpper()))
            {
                x = RowLabels[loc[0].ToString().ToUpper()];
            }
            else
            {
                return null;
            }

            if (!int.TryParse(loc[1].ToString(), out y))
            {
                return null;
            }

            var coords = new Coordinates(x, y);
            if (ValidCoordinates(coords))
            {
                return coords;
            }
            else
            {
                return null;
            }
        }

        private static void InvalidAction(string error)
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

            if (ship.Type == ShipType.Battleship)
            {
                if (Ships.Count(s => s.Type == ShipType.Battleship) == 1)
                {
                    return true;
                }
            }
            else
            {
                if (Ships.Count(s => s.Type == ShipType.Destroyer) == 2)
                {
                    return false;
                }
            }
            return false;
        }

        private List<Coordinates> GetEmptyCells(Coordinates startLoc, Coordinates endLoc)
        {
            var validLocs = new List<Coordinates>();
            if (!ValidCoordinates(startLoc) || !ValidCoordinates(endLoc))
            {
                return null;
            }

            for (int x = startLoc.X; x < endLoc.X; x++)
            {
                if (Coordinates[x, startLoc.Y] > Cell.Miss)
                {
                    return null;
                }
                validLocs.Add(new Coordinates(x, startLoc.Y));
            }
            for (int y = startLoc.Y; y < endLoc.Y; y++)
            {
                if (Coordinates[startLoc.X, y] > Cell.Miss)
                {
                    return null;
                }
                validLocs.Add(new Coordinates(startLoc.X, y));
            }

            return validLocs;
        }

        private static bool ValidCoordinates(Coordinates loc)
        {
            if (loc.Y < 0 || loc.Y >= MaxColumn || loc.X < 0 || loc.X >= MaxRow)
            {
                return false;
            }
            return true;
        }

        public string PrintStatus()
        {
            var output = new StringBuilder();
            output.AppendLine("  " + string.Join(' ', RowLabels.Keys));
            for (var y = 0; y < Coordinates.GetLength(1); y++)
            {
                output.AppendLine();
                output.Append(y.ToString() + " ");
                for (var x = 0; x < Coordinates.GetLength(0); x++)
                {
                    output.Append((int)Coordinates.GetValue(x, y));
                    output.Append(" ");
                }
            }
            return output.ToString();
        }
    }
}