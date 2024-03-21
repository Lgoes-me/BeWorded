﻿using UnityEngine;

public class LevelConfig
{
    public int Score { get; private set; }

    public LevelConfig()
    {
        Score = UnityEngine.Random.Range(300, 1000);
        Debug.Log($"Score: {Score}");
    }
}