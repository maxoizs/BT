using System;
namespace BS
{
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