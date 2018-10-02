using System;
using System.Text;

namespace BS
{
    public class ConsoleBoardDisplayer : IDisplayBoard
    {
        public void DisplayBoard(IBoard board)
        {
            var output = new StringBuilder();
            output.AppendLine("  " + string.Join(' ', RowLabels.Labels.Substring(0, Board.MaxRow).ToCharArray()));
            for (var y = 0; y < board.Coordinates.GetLength(1); y++)
            {
                output.AppendLine();
                output.Append(y.ToString() + " ");
                for (var x = 0; x < board.Coordinates.GetLength(0); x++)
                {
                    output.Append(board.Coordinates[x, y].GetSign());
                    output.Append(" ");
                }
            }
           Log.Output(output.ToString());
        }
    }
}