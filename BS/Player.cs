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
        private Random _rand = new Random();
        private Board _board = new Board();

        /// <summary>
        /// Identify if it's a normal player or computer one.
        /// </summary>
        public bool IsComputer { get; private set; }

        public Player(string name, bool isComputer)
        {
            Name = name;
            IsComputer = isComputer;
            if (IsComputer)
            {
                _board.GenerateShips();
            }
            else
            {
                AddShips();
            }
        }

        internal bool Lost()
        {
            throw new NotImplementedException();
        }

        internal void TakeHit(Coordinates loc)
        {
            _board.TakeHit(loc);
        }

        public Coordinates Hit()
        {
            if (IsComputer)
            {
                return RandomHit();
            }
            return GetHitLocation();
        }

        private Coordinates GetHitLocation()
        {
            Coordinates coords = null;
            while (coords != null)
            {
                Log.Output($"Where you would like to hit, ex: A5 (A for row and 5 for column)");
                var loc = Console.ReadLine();
                coords = Board.GetCoordinations(loc);
            }
            return coords; 
        }

        public void PrintStatus()
        {
            Log.Output(Name);
            _board.PrintStatus();
        }

        private Coordinates RandomHit()
        {
            var row = _rand.Next(1, Board.MaxRow);
            var colum = _rand.Next(1, Board.MaxColumn);
            return new Coordinates(row, colum);
        }

        private void AddShips()
        {
            AddShip( new Destroyer());
            AddShip( new Destroyer());
            AddShip( new Battleship());
        }

        private void AddShip(Ship ship)
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