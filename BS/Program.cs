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
                Playe();
            }

            Log.Output("Good bye");
        }

        private static void Playe()
        {
            throw new NotImplementedException();
        }

        private static void StartNewGame()
        {
            Log.Output("Please enter your name");
            var playerName = Console.ReadLine();
            var game = new Game();
            var player = game.StartSinglePlayerGame(playerName);

            AddShips(player, Ship.Destroyer);
            AddShips(player, Ship.Destroyer);
            AddShips(player, Ship.Battelship);
        }

        private static void AddShips(Player player, Ship ship)
        {
            var added = false;
            while (!added)
            {
                Log.Output($"Where you would like to add your {ship}, ex: A5 (A for row and 5 for column)");
                var location = Console.ReadLine();
                Log.Output("To which direction,[Rr] for toward right, and [Dd] toward down of the grid)");
                var direction =Console.ReadLine();

                added = player.AddPlayerShip(ship, location, direction);
            }
        }       
    }
}
