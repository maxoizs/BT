using System;

namespace BS
{
    /// <summary>
    /// Get Ship Cell value
    /// </summary>
    public static class ShipExtension
    {
        public static Cell ToCell(this Ship ship)
        {
            var shipName = ship.ToString();
            Cell cell;
            Enum.TryParse<Cell>( shipName, out cell);
            return cell;
        }

    }
}