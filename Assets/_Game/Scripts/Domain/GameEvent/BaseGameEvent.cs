using System.Collections.Generic;

public abstract class BaseGameEvent<T> where T : BaseGameEventListener
{
    protected List<T> Listeners { get; set; }

    protected BaseGameEvent()
    {
        Listeners = new List<T>();
    }
    
    public void SubscribeListener(T listener)
    {
        Listeners.Add(listener);
    }

    public void UnsubscribeListener(T listener)
    {
        Listeners.Remove(listener);
    }
}