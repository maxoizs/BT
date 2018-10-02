namespace BS
{
    public interface IBoard
    {
        Cell[,] Coordinates { get; }

        void GenerateShips();
        bool IsLive();
        bool? TakeHit(Coordinates loc);
        bool AddShip(Ship destroyer);
        bool AddShip(Ship ship, Coordinates loc, Direction direction);
    }
}