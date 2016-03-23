using System.Collections.Generic;

namespace WordGame.Database
{
    class FileWordsDataBase : IWordsDataBase
    {
        private const string DefaultFilename = "Resourses/wordlist.txt";
        private readonly HashSet<string> words; 

        public FileWordsDataBase(string filename = DefaultFilename)
        {
            words = new HashSet<string>(System.IO.File.ReadAllLines(filename));
        }

        public bool IsWord(string word)
        {
            return words.Contains(word);
        }
    }
}