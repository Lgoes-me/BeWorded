public class GameEventsManager : BaseManager
{
    public OnLetterScoredGameEvent OnLetterScored { get; set; }
    public OnWordScoredGameEvent OnWordScored { get; set; }

    public void Init()
    {
        OnLetterScored = new OnLetterScoredGameEvent();
        OnWordScored = new OnWordScoredGameEvent();
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
            }
        }
    }
}