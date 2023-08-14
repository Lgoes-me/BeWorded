using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [field: SerializeField] private TextAsset WordsAsset { get; set; }
    private List<string> Words { get; set; }

    private List<(char, int)> WeightedLetters => new()
    {
        ('A', 7), ('B', 3), ('C', 3), ('D', 3), ('E', 5), ('F', 3), ('G', 3), ('H', 1), ('I', 4), ('J', 2), ('K', 0),
        ('L', 3), ('M', 3), ('N', 2), ('O', 7), ('P', 3), ('Q', 1), ('R', 3), ('S', 3), ('T', 3), ('U', 4), ('V', 2),
        ('W', 0), ('X', 1), ('Y', 0), ('Z', 1)
    };

    private List<(IPrize, int)> WeightedPrizes => new()
    {
        (new ScorePrize(10), 32),
        (new ScorePrize(20), 16),
        (new ScorePrize(50), 8),
        (new ScorePrize(100), 4),
        (new ScorePrize(250), 1),
        (new PowerUpPrize(PowerUp.Swap), 2),
        (new PowerUpPrize(PowerUp.Bomb), 2),
        (new PowerUpPrize(PowerUp.Hint), 2),
        (new PowerUpPrize(PowerUp.Shuffle), 1)
    };

    private void Awake()
    {
        //https://github.com/pythonprobr/palavras
        var words = WordsAsset.text.Split("\n");
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
        return word.Length >= 3 && Words.Contains(word);
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