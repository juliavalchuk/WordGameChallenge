using System.Collections.Generic;
using System.Configuration;

namespace WordGame.Database
{
    class FileWordsDataBase : IWordsDataBase
    {
        private readonly HashSet<string> words; 

        public FileWordsDataBase(string filename = null)
        {
            if (filename == null)
                filename = ConfigurationManager.AppSettings["WordListFileName"];

            words = new HashSet<string>(System.IO.File.ReadAllLines(filename));
        }

        public bool IsWord(string word)
        {
            return words.Contains(word);
        }
    }
}