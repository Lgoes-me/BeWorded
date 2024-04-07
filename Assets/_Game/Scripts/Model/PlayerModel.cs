using System;
using System.Collections.Generic;

[Serializable]
public class PlayerModel
{
    public int Level { get; private set; }
    public int Money { get; private set; }
    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }
    public List<JokerModel> Jokers { get; private set; }
    public int QuantidadeJokers { get; private set; }
    public string BaseSeed { get; private set; }
    public int Seed { get; private set; }

    public PlayerModel(
        int level, 
        int money, 
        PowerUp swaps, 
        PowerUp bombs, 
        PowerUp shuffles, 
        List<JokerModel> jokers,
        int quantidadeJokers,
        string baseSeed,
        int seed)
    {
        Level = level;
        Money = money;
        Swaps = swaps;
        Bombs = bombs;
        Shuffles = shuffles;
        Jokers = jokers;
        QuantidadeJokers = quantidadeJokers;
        BaseSeed = baseSeed;
        Seed = seed;
    }
}