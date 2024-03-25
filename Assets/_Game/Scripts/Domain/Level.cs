public class Level
{
    public int Score { get; private set; }
    public int Prize { get; private set; }
    public bool IsFinalLevel { get; }

    public int Tentativas { get; private set; }
    public int CurrentScore { get; private set; }
    public Level(int score, int prize, bool isFinalLevel)
    {
        Score = score;
        Prize = prize;
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