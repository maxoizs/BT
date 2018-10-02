using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    public class Game
    {
        public IDisplayBoard _displayer;
        public Player Player { get; private set; }
        public Player Computer { get; private set; }
        public Player Winner { get; private set; }
        public Game(IDisplayBoard displayer)
        {
            //TODO: remove form here once it's injected to player directly 
            _displayer = displayer;
        }

        public void Start(string playerName)
        {
            var userInput = new UserInput();
            Player = new Player(playerName, userInput, false);
            Computer = new Player("Computer", userInput, true);
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
                    // Finish the game
                    Winner = Player;
                    Log.Output($"{Player.Name} Won the game");
                    Computer.PrintStatus(_displayer);
                    Log.Output($"{Computer.Hits} successful hits and {Computer.Misses} misses");

                    return;
                }

                var computerHit = Computer.Hit();
                Player.TakeHit(computerHit);
                if (Player.Lost())
                {
                    // Finish the game
                    Winner = Computer;
                    Log.Output($"{Computer.Name} Won the game");
                    Log.Output($"{Player.Hits} successful hits and {Player.Misses} misses");
                    Player.PrintStatus(_displayer);
                    return;
                }
            }
        }
    }
}