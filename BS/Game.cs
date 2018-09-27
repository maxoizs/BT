using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    public class Game
    {
        public List<Player> Players { get; private set; }
        private Random _rand = new Random();

        public Game()
        {
            Players = new List<Player>();
        }

        public void Start(string playerName)
        {
            var player = new Player(playerName, false);
            Players.Add(player);
            var computer = new Player("Computer", true);
            Players.Add(computer);

            StartPlay(computer, player);
        }

        private void StartPlay(Player computer, Player player)
        {

        }


    }
}