public class OnPowerUpUsedGameEvent : BaseGameEvent<OnPowerUpUsedListener>
{
    public void Invoke(PowerUp shuffle, params LetterController[] letters)
    {
        foreach (var listener in OrderedListeners)
        {
            listener.OnPowerUpUsed(shuffle, letters);
        }
    }
}