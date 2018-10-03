using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    public class Game
    {
        private IDisplayBoard _displayer;
        private IUserInput _userInput;
        public Player Player { get; private set; }
        private IBoard _playerBoard;
        private IUserInput _computerInput;
        public Player Computer { get; private set; }
        private IBoard _computerBoard;
        public Player Winner { get; private set; }
        public Game(IDisplayBoard displayer, IUserInput userInput, IUserInput computerInput)
        {
            _displayer = displayer;
            _userInput = userInput;
            _computerInput = computerInput; 
        }

        public void Start(string playerName)
        {

            Player = new Player(playerName, _userInput, false);
            _playerBoard = new Board(_userInput);
            
            Computer = new Player("Computer", _computerInput, true);
            _computerBoard = new Board(_computerInput);
            StartPlay();
        }

        private void StartPlay()
        {
            while (true)
            {
                Computer.PrintStatus(_displayer);
                Player.PrintStatus(_displayer);
                var playerHit = Player.Hit();
                Computer.TakeHit(playerHit);

                if (Computer.Lost())
                {
                    GameEnd(Player, Computer);
                    break;
                }

                var computerHit = Computer.Hit();
                Player.TakeHit(computerHit);
                if (Player.Lost())
                {
                    GameEnd(Computer, Player);
                    break;
                }
            }
        }

        private void GameEnd(Player winner, Player loser)
        {
            Winner = winner;
            Log.Output($"{winner.Name} Won the game");
            Log.Output($"{loser.Hits} successful hits and {loser.Misses} misses");
            Player.PrintStatus(_displayer);
            return;
        }
    }
}