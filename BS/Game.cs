using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    public class Game
    {
        public Player Player { get; private set; }
        public Player Computer { get; private set; }

        public Player Winner { get; private set; }
        private Random _rand = new Random();

        public Game()
        {
        }

        public void Start(string playerName)
        {
            Player = new Player(playerName, false);
            Computer = new Player("Computer", true);
            StartPlay();
        }

        private void StartPlay()
        {
            while (true)
            {
                var playerHit= Player.Hit();
                Computer.TakeHit(playerHit);

                if (Computer.Lost())
                {
                    // Finish the game
                    Winner = Player;
                    return;
                }

                var computerHit = Computer.Hit();
                Player.TakeHit(computerHit);
                if (Player.Lost())
                {
                    // Finish the game
                    Winner = Computer;
                    return;
                }
            }
        }

        public void PrintStats()
        {
            Player.PrintStatus();
            Computer.PrintStatus();
        }
    }
}