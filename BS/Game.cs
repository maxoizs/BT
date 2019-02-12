namespace BS
{
    public class Game
    {
        private readonly IDisplayBoard _displayer;

        public Game(IDisplayBoard displayer)
        {
            _displayer = displayer;
        }

        public PlayerManager Player { get; private set; }
        public PlayerManager Computer { get; private set; }
        public PlayerManager Winner { get; private set; }

        public void New(string fstPlayerName, bool fstIsComputer, string secPlayerName, bool secPlayerIsComputer )
        {
            Player = new PlayerManager(fstPlayerName, fstIsComputer);

            Computer = new PlayerManager(secPlayerName, secPlayerIsComputer);
        }

        public void Start()
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
        }
    }
}