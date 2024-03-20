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

    private List<(char, int)> WeightedLetters => new()
    {
        ('A', 8), ('B', 3), ('C', 3), ('D', 3), ('E', 7), ('F', 3), ('G', 3), ('H', 1), ('I', 6), ('J', 2), ('K', 0),
        ('L', 3), ('M', 3), ('N', 2), ('O', 8), ('P', 3), ('Q', 1), ('R', 3), ('S', 3), ('T', 3), ('U', 5), ('V', 2),
        ('W', 0), ('X', 1), ('Y', 0), ('Z', 1)
    };

    private List<(IPrize, int)> WeightedPrizes => new()
    {
        (new ScorePrize(10), 32),
        (new ScorePrize(20), 16),
        (new ScorePrize(50), 8),
        (new ScorePrize(100), 4),
        (new ScorePrize(250), 1),
        (new PowerUpPrize(PowerUp.Troca), 4),
        (new PowerUpPrize(PowerUp.Bomba), 2),
        (new PowerUpPrize(PowerUp.Dica), 2),
        (new PowerUpPrize(PowerUp.Misturar), 1)
    };

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
        var character = WeightedLetters.RandomWeightedElement();
        var prize = WeightedPrizes.RandomWeightedElement();

        return new Letter(character, prize);
    }

    public bool IsValidWord(string word)
    {
        return word.Length >= GameConfig.MinimumWordSize && Words.Contains(word);
    }

    public string GetHint(List<char> letters)
    {
        return Words
            .AsParallel()
            .FirstOrDefault(s => !Enumerable.Except(s, letters).Any()) ?? String.Empty;
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