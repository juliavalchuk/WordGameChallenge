using System;

namespace WordGame
{
    class ConsoleWordsGameView : IWordsGameView
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}