using System.Collections;
using System.Collections.Generic;

public class Joker : ISavable<JokerModel>, IEnumerable<BaseGameEventListener>
{
    public string Id => Identifier.ToString();
    private JokerIdentifier Identifier { get; }
    private List<BaseGameEventListener> GameEventListeners { get; set; }

    public Joker(JokerIdentifier identifier)
    {
        Identifier = identifier;
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
    
    public IEnumerator<BaseGameEventListener> GetEnumerator()
    {
        return GameEventListeners.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

//OnPowerUpUsed
///OnLetterSwaped
///OnBombExploded
///OnBoardShuffled
//OnMatchEnded
//OnItemBought