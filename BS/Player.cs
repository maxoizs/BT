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
        private IUserInput _userInput;
        private IBoard _board;


        /// <summary>
        /// Identify if it's a normal player or computer one.
        /// </summary>
        public bool IsComputer { get; private set; }
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

        public Player(string name, IUserInput userInput, bool isComputer)
        {
            //TODO: inject userinput, and board 
            /* 
            injection made manually and simple as my littel knowledge to dotnet,
             so I don't want to take more time 
             About board: I give to Game and back to player, and I got confused trying to figure out
             Who is responsible for it, I do strongly believe it should be owned by the Game class 
             though I made almost independent and go easily to Game
            */
            _board = new Board(userInput);
            _userInput = userInput;

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
            return !_board.IsLive();
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
            // Todo: Inject IDisplayBoard
            Log.Output(Name);
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
            _board.AddShip(new Destroyer());
            _board.AddShip(new Destroyer());
            _board.AddShip(new Battleship());
        }
    }
}