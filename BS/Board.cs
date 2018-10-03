using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BS
{
    public class Board : IBoard
    {
        private Random _rand = new Random();
        private const int ShipCapacity = 3;
        public int Hits { get; private set; }
        public int Misses { get; private set; }
        private IUserInput _userInput;
        public const int MaxRow = 10;
        public const int MaxColumn = 10;
        public List<Ship> Ships { get; private set; }
        public Cell[,] Coordinates { get; private set; }

        public Board(IUserInput userinput)
        {
            _userInput = userinput;
            Hits = 0;
            Misses = 0;
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


        /// <summary>
        /// Process the hit toward the current <see cref="Board"/>
        /// </summary>
        public bool? TakeHit(Coordinates loc)
        {
            if (!IsLive())
            {
                Log.Output("All ships have been sunk, and player lost already!, please check the correct value");
                return false;
            }

            if (Coordinates[loc.X, loc.Y] == Cell.Destroyer || Coordinates[loc.X, loc.Y] == Cell.Battleship)
            {
                Coordinates[loc.X, loc.Y] = Cell.Hit;
                Hits++;
                return true;
            }

            //Double hit should show as hit 
            Coordinates[loc.X, loc.Y] = Coordinates[loc.X, loc.Y] == Cell.Hit ? Cell.Hit : Cell.Miss;
            Misses++;
            return false;
        }

        /// <summary>
        /// Indicate if <see cref="Board"/> still have active <see cref="Ship"/>s
        /// </summary>
        public bool IsLive()
        {
            var totalShips = Ships.Select(x => (int)x.Type).Sum();

            return Hits < totalShips;
        }

        private void AddShip(Ship ship)
        {
            Log.Output($"Adding ship: {ship}");
            var coords = new Coordinates(-1, -1);
            while (true)
            {
                coords = _userInput.GetCoordinates();
                if (ValidCoordinates(coords))
                {
                    var dir = _userInput.GetDirection();
                    var added = AddShip(ship, coords, dir);
                    if (added)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Add <see cref="Ship" />
        /// </summary>
        /// <returns>true of added, and false if not been added</returns>
        public bool AddShip(Ship ship, Coordinates startLocation, Direction direction)
        {
            if (InvalidCapacity(ship))
            {
                return false;
            }

            var endLocation = GetEndLocation(ship.Size, startLocation, direction);

            var cells = InsureEmptyCells(startLocation, endLocation);

            if (cells == null)
            {
                Log.Output($"Cannot add {ship.Name} on your board at {startLocation} toward {direction}");
                return false;
            }

            UpdateCells(cells, ship);
            Ships.Add(ship);
            Log.Output($"Ship {ship} added on your board at {startLocation} toward {direction}");
            return true;
        }

        /// <summary>
        /// Update Cell with <see cref="Ship.Name"/> shortcut 
        /// </summary>
        private void UpdateCells(List<Coordinates> coordinates, Ship ship)
        {
            foreach (var loc in coordinates)
            {
                Coordinates[loc.X, loc.Y] = ship.ToCell();
            }
        }

        private Coordinates GetEndLocation(int shipSize, Coordinates loc, Direction direction)
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

        /// <summary>
        /// Check Board capacity of ships 
        /// </summary>
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

        /// <summary>
        /// Make sure that position for ship is empty 
        /// </summary>
        private List<Coordinates> InsureEmptyCells(Coordinates startLoc, Coordinates endLoc)
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

        /// <summary>
        /// Ensure <see cref="Coordinates"/> within the range of <see cref="Board"/>
        /// </summary>
        public static bool ValidCoordinates(Coordinates loc)
        {
            if (loc.Y < 0 || loc.Y >= MaxColumn || loc.X < 0 || loc.X >= MaxRow)
            {
                return false;
            }
            return true;
        }

        public void InstallShips()
        {
            AddShip(new Destroyer());
            AddShip(new Destroyer());
            AddShip(new Battleship());
        }
    }
}