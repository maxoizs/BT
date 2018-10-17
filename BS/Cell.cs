using System; 
namespace BS{
    /// <summary>
    /// Cell status
    /// </summary>
    public enum Cell{
        /*
        I tried to construct cell as class that has the ship that occupies it and have functionality like process hit it self 
        and process display flag, but I made it easy enum  */
        Empty=0,        
        Hit=1,
        Miss=2,
        Destroyer=3, 
        Battleship=4,
    }
}