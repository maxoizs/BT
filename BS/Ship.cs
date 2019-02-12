using System.Collections.Generic;

namespace BS
{
    /// <summary>
    /// Ship names with it's size
    /// </summary>
    public abstract class Ship
    {
        public Ship()
        {
            Alive = true;
        }

        public abstract ShipType Type { get; }
        public abstract int Size { get; }
        public bool Alive { get; private set; }

        /// <summary>
        /// Ship <see cref="Coordinates"/>
        /// </summary>
        public List<Coordinates> Positions { get; private set; }

        /// <summary>
        /// index of hits within it's <see cref="Positions"/>
        /// </summary>
        /// <value></value>
        public List<int> Hits { get; private set; }

        public bool TakeHit(Coordinates loc)
        {
            if (!Alive)
            {
                return false;
            }

            if (!Positions.Contains(loc))
            {
                return false;
            }

            Hits.Add(Positions.IndexOf(loc));

            if (Hits.Count == Size)
            {
                Alive = false;
            }

            return true;
        }
    }
}