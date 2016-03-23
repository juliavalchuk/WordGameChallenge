using System;
using System.Linq;

namespace WordGame.Tests
{
    public class TestWordsDataBase : IWordsDataBase
    {
        private string[] _words = new[]
        {
            "test", "base", "word", "yellow", "navy",
            "best", "or", "words", "sword", "as", "worse",
            "a", "testword"
        };

        public bool IsWord(string word)
        {
            return _words.Contains(word);
        }
    }
}