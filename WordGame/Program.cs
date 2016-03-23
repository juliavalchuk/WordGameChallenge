using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Controller;
using WordGame.Database;
using WordGame.Model;
using WordGame.View;

namespace WordGame
{
    public class Program
    {
        // Unfortunately, I had no time to write unit-tests, because spent time remembering C# :(
        // My favorite colors are yellow, navy and purple
        static void Main(string[] args)
        {
            var wordsDataBase = new FileWordsDataBase();
            var model = new WordsGameModel("baseword", wordsDataBase);
            var view = new ConsoleWordsGameView();
            var controller = new ConsoleWordsGameController(model, view);

            controller.Play();
        }
    }
}
