using System;

namespace WordGame.View
{
    class ConsoleWordsGameView : IWordsGameView
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}