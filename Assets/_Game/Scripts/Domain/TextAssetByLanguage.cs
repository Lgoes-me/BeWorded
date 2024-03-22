using System;
using UnityEngine;

[Serializable]
public class TextAssetByLanguage
{
    [field: SerializeField] public TextAsset WordsAsset { get; private set; }
    [field: SerializeField] public LanguageType Language { get; private set; }
}