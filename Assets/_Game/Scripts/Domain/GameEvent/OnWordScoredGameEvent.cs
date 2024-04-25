public class OnWordScoredGameEvent : BaseGameEvent<OnWordScoredListener>
{ 
    public void Invoke(ref int basePrize, ref int baseMultiplier, string word)
    {
        foreach (var listener in OrderedListeners)
        {
            listener.OnWordScored(ref basePrize, ref baseMultiplier, word);
        }
    }
}