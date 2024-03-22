public class Player
{
    public int Level { get; private set; }
    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }

    public Player()
    {
        Level = -1;
        Swaps = new PowerUp(PowerUpType.Troca, 8);
        Bombs = new PowerUp(PowerUpType.Bomba, 4);
        Shuffles = new PowerUp(PowerUpType.Misturar, 1);
    }

    public void PlayLevel()
    {
        Level++;
    }
}