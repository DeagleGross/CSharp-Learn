using System;

namespace Dummy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger(1);
            logger.Log(10, "Hello");
        }
    }

    public class Logger
    {
        private readonly int _level;

        public Logger(int level)
        {
            _level = level;
        }

        public void Log(int level, string message)
        {
            if (level > _level)
            {
                Console.WriteLine(message);
            }
        }
    }
}
