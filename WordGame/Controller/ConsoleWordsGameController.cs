using System;
using WordGame.Model;
using WordGame.View;

namespace WordGame.Controller
{
    public class ConsoleWordsGameController : IWordsGameController
    {
        private readonly IWordsGameModel _wordsGameModel;
        private readonly IWordsGameView _wordsGameView;

        private bool playing;

        public ConsoleWordsGameController(IWordsGameModel wordsGameModel, IWordsGameView wordsGameView)
        {
            _wordsGameModel = wordsGameModel;
            _wordsGameView = wordsGameView;
        }

        public void Play()
        {
            WriteLine("Welcome to the Game!");
            WriteLine("Base word is: " + _wordsGameModel.GetBaseWord());

            playing = true;
            while (playing)
            {
                WriteLine("Enter word to submit. Enter @<number> to get word at position. Enter #<number> to get score at position. Enter * to exit");
                
                var enteredLine = Console.ReadLine();
                StringEntered(enteredLine);
                WriteLine(""); // empty line
            }
            
            WriteLine("Bye!");
            Console.ReadKey();
        }

        private void StringEntered(string userString)
        {
            if (userString.Length == 0)
                return;

            if (userString[0] == '@')
            {
                HandleGetEntryAtPosition(userString);
                return;
            }

            if (userString[0] == '#')
            {
                HandleGetScoreAtPosition(userString);
                return;
            }

            if (userString[0] == '*')
            {
                HandleExit();
                return;
            }

            HandleSubmitWord(userString);
        }

        private void HandleExit()
        {
            playing = false;
        }

        private void HandleSubmitWord(string userString)
        {
            var result = _wordsGameModel.SubmitWord(userString);
            if (result)
            {
                WriteLine("Word submitted");
            }
            else
            {
                WriteLine("Word not submitted");
            }
        }

        private void HandleGetScoreAtPosition(string userString)
        {
            var score = _wordsGameModel.GetScoreAtPosition(GetNumberFromChar(userString[1]));
            if (score == 0)
            {
                WriteLine("Word not found");
            }
            else
            {
                WriteLine(score.ToString());
            }
        }

        private void HandleGetEntryAtPosition(string userString)
        {
            var word = _wordsGameModel.GetWordEntryAtPosition(GetNumberFromChar(userString[1]));
            if (word == null)
            {
                WriteLine("Word not found");
            }
            else
            {
                WriteLine(word);
            }
        }
        
        private void WriteLine(string content)
        {
            _wordsGameView.WriteLine(content);
        }

        private int GetNumberFromChar(char c)
        {
            // maybe need to check input here? But in requirements said that should be 0-9
            return c - '0';
        }
    }
}