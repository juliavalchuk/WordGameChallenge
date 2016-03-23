using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordGame.Tests
{
    [TestClass]
    public class ModelTests
    {
        private IWordsGameModel wordsGameModel;
        private IWordsDataBase wordsDataBase = new TestWordsDataBase();
        private string baseWord = "testbaseword";
        private string[] testWords = new[]
        {
            "test", "base", "word", "best", "or",
            "words", "sword", "as","a", "testword"
        };

        [TestInitialize]
        public void Init()
        {
            wordsGameModel = new WordsGameModel(baseWord, wordsDataBase);
        }

        [TestCleanup]
        public void CleanUp()
        {

        }


        #region tests for GetBaseWord() method

        [TestMethod]
        public void TestGetBaseWord()
        {
            var result = wordsGameModel.GetBaseWord();
            Assert.AreEqual(result, baseWord);
        }

        #endregion

        # region tests for SubmitWord() method
        [TestMethod]
        public void TestSubmitCorrectWord()
        {
            var result = wordsGameModel.SubmitWord("test");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSubmitWrongLettersWord()
        {
            var result = wordsGameModel.SubmitWord("yellow");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestSubmitWrongDictionaryWord()
        {
            var result = wordsGameModel.SubmitWord("store");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestSubmitWordTwice()
        {
            wordsGameModel.SubmitWord("test");
            var result = wordsGameModel.SubmitWord("test");
            Assert.IsFalse(result);
        }

        #endregion

        #region tests for GetWordEntryAtPosition(int position) method

        [TestMethod]
        public void TestGetWordEntryAtPositionWithEmptyList()
        {
            var result = wordsGameModel.GetWordEntryAtPosition(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestGetWordEntryAt0PositionWithOneWord()
        {
            var word = "test";
            wordsGameModel.SubmitWord(word);
            var result = wordsGameModel.GetWordEntryAtPosition(0);
            Assert.IsNotNull(result);
            Assert.AreEqual(word, result);
        }

        [TestMethod]
        public void TestGetWordEntryAt1PositionWithOneWord()
        {
            var word = "test";
            wordsGameModel.SubmitWord(word);
            var result = wordsGameModel.GetWordEntryAtPosition(1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestGetWordEntryAtPositionWithThreeWords()
        {
            var word1 = "as";
            var word2 = "sword";
            var word3 = "word";
            wordsGameModel.SubmitWord(word1);
            wordsGameModel.SubmitWord(word2);
            wordsGameModel.SubmitWord(word3);
            var result1 = wordsGameModel.GetWordEntryAtPosition(0);
            var result2 = wordsGameModel.GetWordEntryAtPosition(1);
            var result3 = wordsGameModel.GetWordEntryAtPosition(2);

            Assert.AreEqual(word2, result1);
            Assert.AreEqual(word3, result2);
            Assert.AreEqual(word1, result3);
        }

        [TestMethod]
        public void TestGetWordEntryAt0PositionWithAddedTenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }

            var expectedResult = "testword";
            var actualResul = wordsGameModel.GetWordEntryAtPosition(0);

            Assert.AreEqual(expectedResult, actualResul);
        }

        [TestMethod]
        public void TestGetWordEntryAt6PositionWithAddedTenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }

            var expectedResult = "best";
            var actualResul = wordsGameModel.GetWordEntryAtPosition(6);

            Assert.AreEqual(expectedResult, actualResul);
        }

        [TestMethod]
        public void TestGetWordEntryAt9PositionWithAddedTenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }

            var expectedResult = "a";
            var actualResul = wordsGameModel.GetWordEntryAtPosition(9);

            Assert.AreEqual(expectedResult, actualResul);
        }

        [TestMethod]
        public void TestGetWordEntryAt9PositionWithAddedElevenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }
            wordsGameModel.SubmitWord("worse");

            var expectedResult = "as";
            var actualResul = wordsGameModel.GetWordEntryAtPosition(9);

            Assert.AreEqual(expectedResult, actualResul);
        }


        #endregion

        #region tests for GetScoreAtPosition(int position) method

        [TestMethod]
        public void TestGetScoreAtPositionWithEmptyList()
        {
            var score = 0;
            var result = wordsGameModel.GetScoreAtPosition(0);
            Assert.AreEqual(score, result);
        }

        [TestMethod]
        public void TestGetScoreAt0PositionWithOneWord()
        {
            var word = "test";
            var score = 4;
            wordsGameModel.SubmitWord(word);
            var result = wordsGameModel.GetScoreAtPosition(0);
            Assert.IsNotNull(result);
            Assert.AreEqual(score, result);
        }

        [TestMethod]
        public void TestGetScoreAt1PositionWithOneWord()
        {
            var word = "test";
            var score = 0;
            wordsGameModel.SubmitWord(word);
            var result = wordsGameModel.GetScoreAtPosition(1);
            Assert.AreEqual(score, result);
        }

        [TestMethod]
        public void TestGetScoreAtPositionWithThreeWords()
        {
            var word1 = "as";
            var word2 = "sword";
            var word3 = "word";
            var score1 = 2;
            var score2 = 5;
            var score3 = 4;
            wordsGameModel.SubmitWord(word1);
            wordsGameModel.SubmitWord(word2);
            wordsGameModel.SubmitWord(word3);
            var result1 = wordsGameModel.GetScoreAtPosition(0);
            var result2 = wordsGameModel.GetScoreAtPosition(1);
            var result3 = wordsGameModel.GetScoreAtPosition(2);

            Assert.AreEqual(score2, result1);
            Assert.AreEqual(score3, result2);
            Assert.AreEqual(score1, result3);
        }

        [TestMethod]
        public void TestGetScoreAt0PositionWithAddedTenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }

            var score = 8;
            var result = wordsGameModel.GetScoreAtPosition(0);

            Assert.AreEqual(score, result);
        }

        [TestMethod]
        public void TestGetScoreAt6PositionWithAddedTenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }

            var score = 4;
            var result = wordsGameModel.GetScoreAtPosition(6);

            Assert.AreEqual(score, result);
        }

        [TestMethod]
        public void TestGetScoreAt9PositionWithAddedTenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }

            var score = 1;
            var result = wordsGameModel.GetScoreAtPosition(9);

            Assert.AreEqual(score, result);
        }

        [TestMethod]
        public void TestGetScoreAt9PositionWithAddedElevenWords()
        {
            foreach (var word in testWords)
            {
                wordsGameModel.SubmitWord(word);
            }
            wordsGameModel.SubmitWord("worse");

            var score = 2;
            var result = wordsGameModel.GetScoreAtPosition(9);

            Assert.AreEqual(score, result);
        }


        #endregion

    }
}
