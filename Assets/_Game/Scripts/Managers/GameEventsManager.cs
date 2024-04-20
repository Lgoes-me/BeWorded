﻿public class GameEventsManager : BaseManager
{
    public OnLetterScoredGameEvent OnLetterScored { get; set; }
    public OnWordScoredGameEvent OnWordScored { get; set; }
    public OnPowerUpUsedGameEvent OnPowerUpUsed { get; set; }
    public OnMatchStartedGameEvent OnMatchStarted { get; set; }

    public void Init()
    {
        OnLetterScored = new OnLetterScoredGameEvent();
        OnWordScored = new OnWordScoredGameEvent();
        OnPowerUpUsed = new OnPowerUpUsedGameEvent();
        OnMatchStarted = new OnMatchStartedGameEvent();
    }

    public void SubscribeJoker(Joker joker)
    {
        foreach (var listener in joker)
        {
            switch (listener)
            {
                case OnLetterScoredListener onLetterScoredListener:
                {
                    OnLetterScored.SubscribeListener(onLetterScoredListener);
                    break;
                }
                
                case OnWordScoredListener onWordScoredListener:
                {
                    OnWordScored.SubscribeListener(onWordScoredListener);
                    break;
                }
                
                case OnPowerUpUsedListener onPowerUpUsedListener:
                {
                    OnPowerUpUsed.SubscribeListener(onPowerUpUsedListener);
                    break;
                }
                
                case OnMatchStartedListener onMatchStartedListener:
                {
                    OnMatchStarted.SubscribeListener(onMatchStartedListener);
                    break;
                }
            }
        }
    }
}