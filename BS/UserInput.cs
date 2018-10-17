using System;

namespace BS
{
    public class UserInput : IPlayerInput
    {
        public Coordinates GetCoordinates()
        {
            Log.Output($"Please Enter a coordinates using letters for rows and number for columns, ex: A5 (A for row and 5 for column)");
            while (true)
            {
                var input = Console.ReadLine();
                var x = -1;
                var y = -1;
                if (input.Length != 2)
                {
                    Log.Error($"Invalid input, Please try again");
                    continue;
                }
                x = RowLabels.GetLabelIndex(input[0].ToString().ToUpper());

                if (!int.TryParse(input[1].ToString(), out y))
                {
                    Log.Error($"Invalid Column vale, Please try again");
                    continue;
                }

                return new Coordinates(x, y);
            }

        }

        public Direction GetDirection()
        {
            Log.Output("To which direction,[Rr] for toward right, and [Dd] toward down of the grid)");
            while (true)
            {
                var direction = Console.ReadLine();
                if (direction.ToLower() == "r")
                {
                    return Direction.Right;
                }
                if (direction.ToLower() == "d")
                {
                    return Direction.Down;
                }
                Log.Error($"Invalid Right/Down direction, Please try again");
            }
        }

    }
}