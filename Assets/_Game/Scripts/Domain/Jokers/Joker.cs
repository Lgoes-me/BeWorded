using System.Collections;
using System.Collections.Generic;

public class Joker : ISavable<JokerModel>, IEnumerable
{
    public string Id => Identifier.ToString();
    public JokerIdentifier Identifier { get; }
    public int SellValue { get; }
    public int Index { get; private set; }
    
    private List<BaseGameEventListener> GameEventListeners { get; set; }

    public Joker(JokerIdentifier identifier)
    {
        Identifier = identifier;
        SellValue = 3;
        Index = 0;
        GameEventListeners = new List<BaseGameEventListener>();
    }

    public void SetIndex(int index)
    {
        Index = index;
        
        foreach (var listener in GameEventListeners)
        {
            listener.SetIndex(index);
        }
    }

    public JokerModel SaveData()
    {
        return new JokerModel(Id, Index);
    }
    
    public void Add(BaseGameEventListener item)
    {
        GameEventListeners.Add(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GameEventListeners.GetEnumerator();
    }
}