using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    public class Game
    {
        private Board _computer;
        private Board _player;

        private Random _rand = new Random();
        public void StartGame()
        {
            _computer = new Board();
            _player = new Board();

            GenerateShips(_computer);
        }

        private void GenerateShips(Board board)
        {
            while (!AddShip(board, Ship.Battelship)) ;
            while (!AddShip(board, Ship.Destroyer)) ;
            while (!AddShip(board, Ship.Destroyer)) ;
        }

        private bool AddShip(Board board, Ship ship)
        {
            var loc = new Coordinates(_rand.Next(Board.MaxRow), _rand.Next(Board.MaxColumn));

            return board.AddShip(loc, ship, (Direction)_rand.Next(1, 2));
        }


        public void AddPlayerShip(Coordinates loc, Ship ship, Direction direction)
        {
            var added = _player.AddShip(loc, ship, direction);
            if (!added)
            {
                Console.Write($"Cannot add {ship} on your board at {loc} toward {direction}");
            }
            else
            {
                Console.Write($"Ship {ship} added on your board at {loc} toward {direction}");
            }
        }
    }
}