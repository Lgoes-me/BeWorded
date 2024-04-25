using System.Collections.Generic;
using System.Linq;

public abstract class BaseGameEvent<T> where T : BaseGameEventListener
{
    protected List<T> Listeners { get; set; }
    protected List<T> OrderedListeners => Listeners.OrderBy(l => l.Index).ToList();

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