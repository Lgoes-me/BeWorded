using System;
using System.Collections.Generic;

[Serializable]
public class ExtraWordsModel
{
    public List<string> Words { get; private set; }

    public ExtraWordsModel(List<string> words)
    {
        Words = words;
    }
}