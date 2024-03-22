using UnityEngine;

public class LevelConfig
{
    public int Score { get; private set; }

    public LevelConfig(int score)
    {
        Score = score;
        Debug.Log($"Score: {Score}");
    }
}