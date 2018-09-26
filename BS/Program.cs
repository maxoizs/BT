using System;

namespace BS
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Output("Welcom to Battel Ship game!");
            Log.Output("Would you like to start a game ? [Yy] for yes, anykey for no");
            var newGame = Console.ReadKey().KeyChar;
            if (newGame.ToString().ToLower() == "y")
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
            game.StartSinglePlayerGame(playerName);           
        }        
    }
}
