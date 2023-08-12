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
    public PowerUp PowerUp  { get; }

    public PowerUpPrize(PowerUp powerUp)
    {
        PowerUp = powerUp;
    }

    public new string ToString()
    {
        return PowerUp.ToString();
    }
}

public enum PowerUp
{
    Swap,
    Bomb,
    Hint,
    Shuffle
}