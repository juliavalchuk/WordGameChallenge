using System.Collections.Generic;
using System.Configuration;
using WordGame.Database;

namespace WordGame.Model
{
    public class WordsGameModel : IWordsGameModel
    {
        private readonly string baseWord;
        private readonly IWordsDataBase wordsDataBase;
        private readonly Dictionary<char, int> baseLetters;
        private List<WordScore> topWords = new List<WordScore>();
        private int MaxWordsCount;

        public WordsGameModel(string baseWord, IWordsDataBase wordsDataBase)
        {
            this.baseWord = baseWord;
            this.wordsDataBase = wordsDataBase;
            baseLetters = GetLettersDictionary(baseWord);
            ReadMaxWordsCountFromConfig();
        }

        public string GetBaseWord()
        {
            return this.baseWord;
        }

        public bool SubmitWord(string word)
        {
            return IsInBaseLetters(word) && IsInDataBase(word) && AddToTopWords(word);
        }

        public string GetWordEntryAtPosition(int position)
        {
            if (position >= topWords.Count)
            {
                return null;
            }

            return topWords[position].Word;
        }

        public int GetScoreAtPosition(int position)
        {
            if (position >= topWords.Count)
            {
                return 0;
            }

            return topWords[position].Score;
        }

        private Dictionary<char, int> GetLettersDictionary(string letters)
        {
            Dictionary<char, int> lettersDictionary = new Dictionary<char, int>();
            foreach (var letter in letters)
            {
                if (!lettersDictionary.ContainsKey(letter))
                {
                    lettersDictionary.Add(letter, 1);
                }
                else
                {
                    lettersDictionary[letter]++;
                }
            }

            return lettersDictionary;
        }

        private bool IsInBaseLetters(string word)
        {
            var wordLetters = GetLettersDictionary(word);

            foreach (var wordLetter in wordLetters)
            {
                if (!baseLetters.ContainsKey(wordLetter.Key) || baseLetters[wordLetter.Key] < wordLetter.Value)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsInDataBase(string word)
        {
            return wordsDataBase.IsWord(word);
        }

        private int GetScore(string word)
        {
            return word.Length;
        }

        private bool AddToTopWords(string word)
        {
            int score = GetScore(word);
            bool wordAdded = false;

            for (var i = 0; i < topWords.Count; i++)
            {
                var wordScore = topWords[i];
                if (wordScore.Word == word)
                {
                    return false;
                }

                if (wordScore.Score < score)
                {
                    wordAdded = true;
                    topWords.Insert(i, new WordScore() { Score = score, Word = word });
                    break;
                }
            }

            if (!wordAdded)
            {
                topWords.Add(new WordScore() { Score = score, Word = word });
            }

            while (topWords.Count > MaxWordsCount)
            {
                topWords.RemoveAt(topWords.Count - 1);
            }
            
            return true;
        }

        public void ReadMaxWordsCountFromConfig()
        {
            if(int.TryParse(ConfigurationManager.AppSettings["MaxWordCount"], out MaxWordsCount) == false)
            {
                MaxWordsCount = 10; // default
            }
        }
    }
}