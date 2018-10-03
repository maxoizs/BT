using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    /// <summary>
    /// <see cref="Player" Manager/>
    /// </summary>
    public class PlayerManager
    {
        public IPlayer Player { get; private set; }
        private Random _rand = new Random();
        private IUserInput _userInput;
        private IBoard _board;

        public int Hits
        {
            get
            {
                return _board != null ? _board.Hits : 0;
            }
        }

        public object Misses
        {
            get
            {
                return _board != null ? _board.Misses : 0;
            }
        }

        public PlayerManager(string name, IUserInput userInput, bool isComputer)
        {
            _board = new Board(userInput);
            _userInput = userInput;
            Player = new Player(name, isComputer);
            AddShips();
        }

        internal bool Lost()
        {
            return !_board.IsLive();
        }

        internal void TakeHit(Coordinates loc)
        {
            _board.TakeHit(loc);
        }

        public Coordinates Hit()
        {
            if (Player.IsComputer)
            {
                return RandomHit();
            }
            return GetHitLocation();
        }

        private Coordinates GetHitLocation()
        {
            Coordinates coords = null;
            Log.Output($"Where you would like to hit");
            while (true)
            {
                coords = _userInput.GetCoordinates();
                if (Board.ValidCoordinates(coords))
                {
                    break;
                }
            }
            return coords;
        }

        /// <summary>
        /// Display the player board using the givin displayer adapter
        /// </summary>
        public void PrintStatus(IDisplayBoard displayer)
        {
            Player.PrintDetails();
            displayer.DisplayBoard(_board);
        }

        private Coordinates RandomHit()
        {
            var row = _rand.Next(1, Board.MaxRow);
            var column = _rand.Next(1, Board.MaxColumn);
            return new Coordinates(row, column);
        }

        private void AddShips()
        {
            _board.InstallShips();
        }
    }
}