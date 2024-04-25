using System.Collections;
using System.Collections.Generic;

public abstract class BaseGameEventListener
{
    public int Index { get; private set; }

    public void SetIndex(int index)
    {
        Index = index;
    }
}

public abstract class BaseGameEventListener<T, T1>: BaseGameEventListener, IEnumerable
{
    protected List<T> Validators { get; set; }
    protected List<T1> Modifiers { get; set; }

    protected BaseGameEventListener()
    {
        Validators = new List<T>();
        Modifiers = new List<T1>();
    }

    public void Add(T validator)
    {
        Validators.Add(validator);
    }
    
    public void Add(T1 modifier)
    {
        Modifiers.Add(modifier);
    }

    public IEnumerator GetEnumerator()
    {
        return Modifiers.GetEnumerator();
    }
}