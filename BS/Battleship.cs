using System.Collections.Generic;

namespace BS
{
    public class Battleship : Ship
    {
        public override string Name => "Battleship";
        public override int Size => 5;
        public override ShipType Type => ShipType.Battleship;
    }
}