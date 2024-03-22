using UnityEngine;

public class Level
{
    public int Score { get; private set; }
    public int CurrentScore { get; private set; }

    public Level(int score)
    {
        Score = score;
        CurrentScore = 0;
        Debug.Log($"Score: {Score}");
    }

    public void GiveScore(int score)
    {
        CurrentScore += score;
    }
}