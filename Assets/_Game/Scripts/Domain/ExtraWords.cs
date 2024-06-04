using System.Collections.Generic;

public class ExtraWords : ISavable<ExtraWordsModel>, ILoadable<ExtraWordsModel>
{
    public string Id { get; set; }
    private List<string> Words { get; set; }

    public ExtraWords()
    {
        Id = "ExtraWords.json";
        Words = new List<string>();
    }

    public bool Contains(string word)
    {
        return Words.Contains(word);
    }

    public void Add(string word)
    {
        Words.Add(word);
    }
    
    public void LoadData(ExtraWordsModel data)
    {
        Words = data.Words;
    }

    public ExtraWordsModel SaveData()
    {
        return new ExtraWordsModel(Words);
    }
}