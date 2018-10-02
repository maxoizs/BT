namespace BS
{
    public interface IBoard
    {
        Cell[,] Coordinates { get; }
        int Hits { get; }
        int Misses { get; }

        /// <summary>
        /// Generate Ships in random position for Computer player 
        /// </summary>
        void GenerateShips();
        bool IsLive();

        /// <summary>
        /// Process the hit toward the current <see cref="Board"/>
        /// </summary>
        bool? TakeHit(Coordinates loc);
        void AddShip(Ship destroyer);
        bool AddShip(Ship ship, Coordinates loc, Direction direction);
    }
}