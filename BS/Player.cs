namespace BS
{
    /// <summary>
    /// <see cref="Game" Player/>
    /// </summary>
    public class Player : IPlayer
    {
        public Player(string name, bool isComputer)
        {
            Name = name;
            IsComputer = isComputer;
        }

        public string Name { get; }

        /// <summary>
        /// Identify if it's a normal player or computer one.
        /// </summary>
        public bool IsComputer { get; }

        /// <summary>
        /// Display the player board using the givin displayer adapter
        /// </summary>
        public void PrintDetails()
        {
            Log.Output(Name);
        }
    }
}