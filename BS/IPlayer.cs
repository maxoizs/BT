using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS
{
    /// <summary>
    /// <see cref="Game" IPlayer/>
    /// </summary>
    public interface IPlayer
    {
        string Name { get; }

        /// <summary>
        /// Identify if it's a normal player or computer one.
        /// </summary>
        bool IsComputer { get; }

        /// <summary>
        /// Display the player board using the givin displayer adapter
        /// </summary>
        void PrintDetails();

    }
}