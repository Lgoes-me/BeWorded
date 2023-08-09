using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

public class ContentManager : MonoBehaviour
{    
    [field: SerializeField] private TextAsset WordsAsset { get; set; }
    private List<string> ListOfAllWords { get; set; }

    private void Awake()
    {
        //https://github.com/pythonprobr/palavras
        var text = WordsAsset.text;
        var AllWords = text.Split("\n");
        ListOfAllWords =  new List<string>(AllWords).Select(RemoveAccents).ToList();
    }

    public string GetRandomLetter()
    {
        char[] chars = "ABCDEFGHIJLMNOPQRSTUVXZ".ToCharArray();
        return chars[UnityEngine.Random.Range(0, chars.Length)].ToString();
    }
    
    public bool IsValidWord(string word)
    {
        return ListOfAllWords.Contains(word.ToLower());
    }
    
    private string RemoveAccents(string text){   
        StringBuilder sbReturn = new StringBuilder();   
        var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
        foreach (char letter in arrayText){   
            if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                sbReturn.Append(letter);   
        }   
        return sbReturn.ToString();   
    } 
}