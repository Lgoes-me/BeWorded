public class Level
{
    public int Score { get; private set; }
    public int Tentativas { get; private set; }
    public int CurrentScore { get; private set; }
    public bool IsFinalLevel { get; }

    public Level(int score, bool isFinalLevel)
    {
        Score = score;
        Tentativas = 5;
        CurrentScore = 0;
        IsFinalLevel = isFinalLevel;
    }

    public void GiveScore(int score)
    {
        CurrentScore += score;
    }
    

    public void UsarTentativa()
    {
        Tentativas--;
    }
}