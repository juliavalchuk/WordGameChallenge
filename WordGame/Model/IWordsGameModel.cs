namespace WordGame.Model
{
    public interface IWordsGameModel
    {
        string GetBaseWord();
        bool SubmitWord(string word);
        string GetWordEntryAtPosition(int position);
        int GetScoreAtPosition(int position);
    }
}