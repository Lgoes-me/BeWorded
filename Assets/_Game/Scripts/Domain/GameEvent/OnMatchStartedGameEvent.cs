public class OnMatchStartedGameEvent : BaseGameEvent<OnMatchStartedListener>
{
    public void Invoke()
    {
        foreach (var listener in Listeners)
        {
            listener.OnMatchStarted();
        }
    }
}