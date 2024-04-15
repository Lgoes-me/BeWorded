using System;
using System.Linq;

public class OnLetterScoredListener : BaseGameEventListener<Func<Letter, bool>, LetterScoredDelegate>
{
    public void OnLetterScored(ref int points, ref int multiplier, Letter letter)
    {
        if (Validators.Any(validator => !validator(letter)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier(ref points, ref multiplier);
        }
    }
}

public delegate void LetterScoredDelegate(ref int points, ref int multiplier);