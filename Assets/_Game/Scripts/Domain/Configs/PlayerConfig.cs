public class PlayerConfig
{
    private int Money { get; }
    private int Swaps { get; }
    private int Bombs { get; }
    private int Shuffles { get; }
    private int QuantidadeJokers { get; }

    public PlayerConfig()
    {
        Money = 4;
        Swaps = 8;
        Bombs = 4;
        Shuffles = 1;
        QuantidadeJokers = 5;
    }

    public Player CreatePlayer(string seed = "")
    {
        return new Player(Money, Swaps, Bombs, Shuffles, QuantidadeJokers, seed);
    }
}