using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [field: SerializeField] private List<TextAssetByLanguage> Texts { get; set; }
    private List<string> Words { get; set; }
    private GameConfig GameConfig { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void Init(GameConfig gameConfig)
    {
        GameConfig = gameConfig;
        
        //https://github.com/pythonprobr/palavras
        var words = Texts.First(x => x.Language == gameConfig.GameLanguage).WordsAsset.text.Split("\n");
        
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