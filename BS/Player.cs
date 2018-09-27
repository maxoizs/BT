using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    /// <summary>
    /// <see cref="Game" Player/>
    /// </summary>
    public class Player
    {
        public string Name { get; private set; }
        private Board _board;

        /// <summary>
        /// Identify if it's a normal player or computer one.
        /// </summary>
        public bool IsComputer { get; private set; }

        public Player(string name, bool isComputer)
        {
            Name = name;
            IsComputer = isComputer;
            _board = new Board();
            if (IsComputer)
            {
                _board.GenerateShips();
            }
            else
            {
                AddShips();
            }
        }

        public void PrintStats()
        {
            Log.Output(_board.GetStats());
        }

        public List<Ship> Ships()
        {
            return _board.Ships.ToList();
        }

        public int Hits()
        {
            return 0;
        }
        public int Misses()
        {
            return 0;
        }
        public bool AddShip(Ship ship, Coordinates loc, Direction direction)
        {
            return _board.AddShip(ship, loc, direction);
        }

        public bool AddShip(Ship ship, string loc, string direction)
        {
            return _board.AddShip(ship, loc, direction);
        }

        public bool TakeHit(string loc)
        {
            return _board.TakeHit(loc);
        }
        private void AddShips()
        {
            AddShip(new Destroyer());
            AddShip(new Destroyer());
            AddShip(new Battleship());
        }

        public void AddShip(Ship ship)
        {
            var added = false;
            while (!added)
            {
                Log.Output($"Where you would like to add your {ship}, ex: A5 (A for row and 5 for column)");
                var location = Console.ReadLine();
                Log.Output("To which direction,[Rr] for toward right, and [Dd] toward down of the grid)");
                var direction = Console.ReadLine();

                added = _board.AddShip(ship, location, direction);
            }
        }
    }
}