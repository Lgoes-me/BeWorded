public interface IPrize
{
    string ToString();
}

public class ScorePrize : IPrize
{
    public int Score  { get; }

    public ScorePrize(int score)
    {
        Score = score;
    }

    public new string ToString()
    {
        return Score.ToString();
    }
}

public class PowerUpPrize : IPrize
{
    public PowerUpType PowerUp  { get; }

    public PowerUpPrize(PowerUpType powerUp)
    {
        PowerUp = powerUp;
    }

    public new string ToString()
    {
        return PowerUp.ToString();
    }
}