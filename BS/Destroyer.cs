using System.Collections.Generic;

namespace BS
{
    public class Destroyer : Ship
    {
        public override int Size => 2;
        public override ShipType Type => ShipType.Destroyer;
    }
}