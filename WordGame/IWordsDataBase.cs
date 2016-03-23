using System;
using System.Collections.Generic;

namespace WordGame
{
    public interface IWordsDataBase
    {
        bool IsWord(string word);
    }

    class FileWordsDataBase : IWordsDataBase
    {
        private readonly HashSet<string> words; 

        public FileWordsDataBase(string filename = "wordlist.txt")
        {
            words = new HashSet<string>(System.IO.File.ReadAllLines(filename));
        }

        public bool IsWord(string word)
        {
            return words.Contains(word);
        }
    }
}