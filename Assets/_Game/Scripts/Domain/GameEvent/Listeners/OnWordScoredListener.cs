using System;
using System.Linq;

public class OnWordScoredListener  : BaseGameEventListener<Func<string, bool>, WordScoredDelegate>
{
    public void OnWordScored(ref int points, ref int multiplier, string word)
    {
        if (Validators.Any(validator => !validator(word)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier(ref points, ref multiplier);
        }
    }
}

public delegate void WordScoredDelegate(ref int points, ref int multiplier);
