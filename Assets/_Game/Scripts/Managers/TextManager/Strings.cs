using System;
using System.Collections.Generic;
using System.Linq;

public class Strings
{
    private Dictionary<string, TextKeyData> TextKeys { get; set; }

    public Strings(List<TextKeyData> textKeys)
    {
        TextKeys = textKeys.ToDictionary(t => t.TextKey, t => t);
    }

    public string GetText(string textKey, LanguageType languageType)
    {
        if (!TextKeys.TryGetValue(textKey, out var textData))
            return textKey;
        
        return languageType switch
        {
            LanguageType.Pt => textData?.Pt ?? textKey,
            LanguageType.En => textData?.En ?? textKey,
            _ => throw new ArgumentOutOfRangeException(nameof(languageType), languageType, null)
        };
    }
}

public class TextKeyData
{
    public string TextKey { get; private set; }
    public string Pt { get; private set; }
    public string En { get; private set; }

    public TextKeyData(string textKey, string pt, string en)
    {
        TextKey = textKey;
        Pt = pt;
        En = en;
    }
}