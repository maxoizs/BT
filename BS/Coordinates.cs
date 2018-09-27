using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BS
{
   
    /// <summary>
    /// <see cref="Cell"/> Location 
    /// </summary>
 public class Coordinates: IEquatable<Coordinates>
    {
        public int X { get; private set; }
        public int Y { get; private set; }


        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinates other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinates)obj);
        }

        public override int GetHashCode()
        {
            unchecked { return (X.GetHashCode() * 397) ^ Y.GetHashCode(); }
        }
    }
}