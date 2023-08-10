using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [field: SerializeField] private TextAsset WordsAsset { get; set; }
    private string[] Words { get; set; }

    private List<(char, int)> WeightedLetters => new()
    {
        ('A', 4), ('B', 3), ('C', 3), ('D', 3), ('E', 4), ('F', 3), ('G', 3), ('H', 1), ('I', 3), ('J', 2), ('K', 0),
        ('L', 3), ('M', 3), ('N', 2), ('O', 4), ('P', 3), ('Q', 1), ('R', 3), ('S', 3), ('T', 3), ('U', 3), ('V', 2),
        ('W', 0), ('X', 1), ('Y', 0), ('Z', 1)
    };

    private void Awake()
    {
        //https://github.com/pythonprobr/palavras
        var words = WordsAsset.text.Split("\n");
        Words = new List<string>(words).Select(RemoveAccents).ToArray();
    }

    public string GetRandomLetter()
    {
        var letters = new List<char> ();

        foreach (var (letter, weight) in WeightedLetters)
        {
            var count = weight;
            while (count != 0)
            {
                letters.Add(letter);
                count--;
            }
        }
        
        return letters[Random.Range(0, letters.Count)].ToString();
    }

    public bool IsValidWord(string word)
    {
        return Words.Contains(word.ToLower());
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