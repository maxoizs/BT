using System;
namespace BS
{
    /// <summary>
    /// Log output for console or for testing 
    /// </summary>
    public class Log
    {
        public static void Output(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine("---------");
        }
    }
}