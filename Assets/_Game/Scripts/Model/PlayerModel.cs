using System;

[Serializable]
public class PlayerModel
{
    public int Level { get; private set; }
    public int Money { get; private set; }
    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }

    public PlayerModel(int level, int money, PowerUp swaps, PowerUp bombs, PowerUp shuffles)
    {
        Level = level;
        Money = money;
        Swaps = swaps;
        Bombs = bombs;
        Shuffles = shuffles;
    }
}