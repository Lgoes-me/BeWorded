public class Player
{
    public int Level { get; private set; }
    public int Swaps { get; private set; }
    public int Bombs { get; private set; }
    public int Shuffles { get; private set; }

    public Player()
    {
        Level = -1;
        Swaps = 8;
        Bombs = 4;
        Shuffles = 1;
    }

    public void PlayLevel()
    {
        Level++;
    }
}