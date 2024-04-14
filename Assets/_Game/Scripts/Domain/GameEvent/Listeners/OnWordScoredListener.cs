using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OnWordScoredListener : BaseGameEventListener, IEnumerable<Func<string, bool>>, IEnumerable<WordScoredDelegate>
{
    private List<Func<string, bool>> Validators { get; set; }
    private List<WordScoredDelegate> Modifiers { get; set; }

    public OnWordScoredListener()
    {
        Validators = new List<Func<string, bool>>();
        Modifiers = new List<WordScoredDelegate>();
    }

    public void OnWordScored(ref int points, ref int multiplier, string word)
    {
        if (Validators.Any(validator => !validator(word)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier(ref points, ref multiplier);
        }
    }

    public void Add(Func<string, bool> validator)
    {
        Validators.Add(validator);
    }
    
    public IEnumerator<Func<string, bool>> GetEnumerator()
    {
        return Validators.GetEnumerator();
    }
    
    public void Add(WordScoredDelegate modifier)
    {
        Modifiers.Add(modifier);
    }
    
    IEnumerator<WordScoredDelegate> IEnumerable<WordScoredDelegate>.GetEnumerator()
    {
        return Modifiers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public delegate void WordScoredDelegate(ref int points, ref int multiplier);
