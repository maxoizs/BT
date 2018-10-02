using System;
using System.Reflection;
using Ninject;

namespace BS
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Output("Welcome to Battel Ship game!");
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

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            Log.Output("Please enter your name");
            var playerName = Console.ReadLine();
            var game = kernel.Get<Game>();

            game.Start(playerName);
        }
    }
}
