using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class OnLetterScoredListener : BaseGameEventListener, IEnumerable<Func<Letter, bool>>,
    IEnumerable<LetterScoredDelegate>
{
    private List<Func<Letter, bool>> Validators { get; set; }
    private List<LetterScoredDelegate> Modifiers { get; set; }

    internal OnLetterScoredListener()
    {
        Validators = new List<Func<Letter, bool>>();
        Modifiers = new List<LetterScoredDelegate>();
    }

    public void OnLetterScored(ref int points, ref int multiplier, Letter letter)
    {
        if (Validators.Any(validator => !validator(letter)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier(ref points, ref multiplier);
        }
    }

    public void Add(Func<Letter, bool> validator)
    {
        Validators.Add(validator);
    }

    public IEnumerator<Func<Letter, bool>> GetEnumerator()
    {
        return Validators.GetEnumerator();
    }

    public void Add(LetterScoredDelegate modifier)
    {
        Modifiers.Add(modifier);
    }

    IEnumerator<LetterScoredDelegate> IEnumerable<LetterScoredDelegate>.GetEnumerator()
    {
        return Modifiers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public delegate void LetterScoredDelegate(ref int points, ref int multiplier);