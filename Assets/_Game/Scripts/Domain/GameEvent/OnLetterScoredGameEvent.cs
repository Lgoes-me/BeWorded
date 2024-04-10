public class OnLetterScoredGameEvent : BaseGameEvent<OnLetterScoredListener>
{
    public void Invoke(ref int basePrize, ref int baseMultiplier, Letter letter)
    {
        foreach (var listener in Listeners)
        {
            listener.OnLetterScored(ref basePrize, ref baseMultiplier, letter);
        }
    }
}