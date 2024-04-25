public class OnLetterScoredGameEvent : BaseGameEvent<OnLetterScoredListener>
{
    public void Invoke(ref int basePrize, ref int baseMultiplier, Level level, Letter letter)
    {
        foreach (var listener in OrderedListeners)
        {
            listener.OnLetterScored(ref basePrize, ref baseMultiplier, level, letter);
        }
    }
}