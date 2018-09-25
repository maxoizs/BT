using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    public class Game
    {
        private Player _computer;

        public List<Player> Players { get; private set; }
        private Random _rand = new Random();

        public Game(){
            Players = new List<Player>();
        }

        public void StartMultiPlayerGame(string playerName, string player2Name)
        {
            Players.Add(new Player(playerName, false));
            Players.Add(new Player(player2Name, false));
        }

        public Player StartSinglePlayerGame(string playerName)
        {
            var player = new Player(playerName, false); 
            Players.Add(player);
            _computer = new Player("Computer", true);
            Players.Add(_computer);
            return player;
        }
    }
}