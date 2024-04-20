using System.Collections;
using System.Collections.Generic;

public class Joker : ISavable<JokerModel>, IEnumerable
{
    public string Id => Identifier.ToString();
    public JokerIdentifier Identifier { get; }
    public int SellValue { get; }
    
    private List<BaseGameEventListener> GameEventListeners { get; set; }

    public Joker(JokerIdentifier identifier)
    {
        Identifier = identifier;
        SellValue = 3;
        GameEventListeners = new List<BaseGameEventListener>();
    }

    public JokerModel SaveData()
    {
        return new JokerModel(Id);
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