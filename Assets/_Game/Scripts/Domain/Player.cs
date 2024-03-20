public class Player
{
    public int Swaps { get; private set; }
    public int Bombs { get; private set; }
    public int Hints { get; private set; }
    public int Shuffles { get; private set; }

    public Player()
    {
        Swaps = 8;
        Bombs = 4;
        Hints = 2;
        Shuffles = 1;
    }
}