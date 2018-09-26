using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BS
{
    /// <summary>
    /// <see cref="Cell"/> Location 
    /// </summary>
    public class Coordinates
    {
        public int X { get;private set; }
        public int Y { get;private set; }

        
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }        
    }
}