namespace BS
{
    public class RowLabels
    {
        public const string Labels = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Return the index value of character within the <see cref="Labels"/> string. 
        /// </summary>
        /// <param name="label"></param>
        /// <returns>-1 if not found, 0 or more if found.</returns>
        public static int GetLabelIndex(string label)
        {
            if (Labels.Contains(label))
            {
                return Labels.IndexOf(label);
            }

            return -1;
        }
    }
}