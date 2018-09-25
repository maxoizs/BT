using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    public class Player
    {        
        public string Name { get; private set; }
        private Board _board;

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
        }
        
        public bool AddPlayerShip(Ship ship, Coordinates loc, Direction direction)
        {
            return _board.AddShip(ship, loc, direction);
        }

        public bool AddPlayerShip(Ship ship, string loc, string direction)
        {
            return _board.AddShip(ship, loc, direction);
        }

        public bool TakeHit(string loc){
            return _board.TakeHit(loc);
        }
    }
}