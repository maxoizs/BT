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
        public PlayerManager Player { get; private set; }
        private IUserInput _computerInput;
        public PlayerManager Computer { get; private set; }
        public PlayerManager Winner { get; private set; }
        public Game(IDisplayBoard displayer, IUserInput userInput, IUserInput computerInput)
        {
            _displayer = displayer;
            _userInput = userInput;
            _computerInput = computerInput; 
        }

        public void Start(string playerName)
        {

            Player = new PlayerManager(playerName, _userInput, false);
            
            Computer = new PlayerManager("Computer", _computerInput, true);
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

        private void GameEnd(PlayerManager winner, PlayerManager loser)
        {
            Winner = winner;
            Log.Output($"{winner.Player.Name} Won the game");
            Log.Output($"{loser.Hits} successful hits and {loser.Misses} misses");
            Player.PrintStatus(_displayer);
            return;
        }
    }
}