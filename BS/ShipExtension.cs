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
            Cell cell;
            Enum.TryParse(ship.Type.ToString(), out cell);
            return cell;
        }
    }
}