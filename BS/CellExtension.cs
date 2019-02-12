namespace BS
{
    public static class CellExtension
    {
        public static string GetSign(this Cell cell)
        {
            switch (cell)
            {
                case Cell.Battleship:
                    return "B";
                case Cell.Destroyer:
                    return "D";
                case Cell.Empty:
                    return "-";
                case Cell.Hit:
                    return "1";
                case Cell.Miss:
                    return "0";
                default:
                    return string.Empty;
            }
        }
    }
}