using System;

[Serializable]
public class GameConfigModel
{
    public LanguageType Language { get; private set; }

    public GameConfigModel(LanguageType language)
    {
        Language = language;
    }
}