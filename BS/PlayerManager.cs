﻿using System;
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
        private IPlayerInput _userInput;
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

        public PlayerManager(string name, bool isComputer)
        {
            _userInput = isComputer ? new ComputerInput() : _userInput = new UserInput();
            _board = new Board(_userInput);
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
            return _userInput.GetCoordinates();
        }

        /// <summary>
        /// Display the player board using the givin displayer adapter
        /// </summary>
        public void PrintStatus(IDisplayBoard displayer)
        {
            Player.PrintDetails();
            displayer.DisplayBoard(_board);
        }


        private void AddShips()
        {
            _board.InstallShips();
        }
    }
}