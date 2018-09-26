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

        public Game(){
            Players = new List<Player>();
        }

        public void StartMultiPlayerGame(string playerName, string player2Name)
        {
            Players.Add(new Player(playerName, false));
            Players.Add(new Player(player2Name, false));
        }

        public void StartSinglePlayerGame(string playerName)
        {
            var player = new Player(playerName, false); 
            Players.Add(player);
            var computer = new Player("Computer", true);
            Players.Add(computer);

            StartSinglePlay(computer, player);
        }

        private void StartSinglePlay(Player computer, Player player)
        { 
            var finish = false; 
          
            
        }


    }
}