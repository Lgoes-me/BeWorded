public class OnMatchStartedGameEvent : BaseGameEvent<OnMatchStartedListener>
{
    public void Invoke()
    {
        foreach (var listener in OrderedListeners)
        {
            listener.OnMatchStarted();
        }
    }
}