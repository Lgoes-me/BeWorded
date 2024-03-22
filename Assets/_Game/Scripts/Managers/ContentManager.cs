using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

public class ContentManager : BaseManager
{
    [field: SerializeField] private List<TextAssetByLanguage> Texts { get; set; }
    
    private List<string> Words { get; set; }
    private GameConfig GameConfig { get; set; }

    public void Init()
    {
        GameConfig = Application.ConfigManager.GameConfig;
        
        //https://github.com/pythonprobr/palavras
        //https://github.com/dwyl/english-words
        var words = Texts.First(x => x.Language == GameConfig.Language).WordsAsset.text.Split("\n");
        
        Words = new List<string>(words)
            .AsParallel()
            .Select(s => RemoveAccents(s).ToUpper().Replace("\r", ""))
            .Where(s => s.Length >= 3)
            .OrderBy(s => s.Length)
            .ToList();
    }

    public Letter GetRandomLetter()
    {
        var character = GameConfig.WeightedLetters.RandomWeightedElement();
        var prize = GameConfig.WeightedPrizes.RandomWeightedElement();

        return new Letter(character, prize);
    }

    public bool IsValidWord(string word)
    {
        return word.Length >= GameConfig.MinimumWordSize && Words.Contains(word);
    }

    private string RemoveAccents(string text)
    {
        StringBuilder sbReturn = new StringBuilder();
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

        foreach (char letter in arrayText)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);
        }

        return sbReturn.ToString();
    }
}