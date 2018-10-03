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
        public Player Computer { get; private set; }
        public Player Winner { get; private set; }
        public Game(IDisplayBoard displayer, IUserInput userInput)
        {
            _displayer = displayer;
            _userInput = userInput;
        }

        public void Start(string playerName)
        {
           
            Player = new Player(playerName, _userInput, false);
            Computer = new Player("Computer", _userInput, true);
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