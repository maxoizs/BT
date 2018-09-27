using System;

namespace BS
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Output("Welcom to Battel Ship game!");
            Log.Output("Would you like to start a game ? [Yy] for yes, anykey for no");
            var newGame = Console.ReadLine();
            if (newGame.ToLower() == "y")
            {
                StartNewGame();
            }

            Log.Output("Good bye");
        }

        private static void StartNewGame()
        {
            Log.Output("Please enter your name");
            var playerName = Console.ReadLine();
            var game = new Game();
player.AddShip(new Destroyer());
            player.AddShip(new Destroyer());
            player.AddShip(new Battleship());
            game.StartSinglePlayerGame(playerName);    
            game.Players[0].PrintStats();       
        }        
    }
}
