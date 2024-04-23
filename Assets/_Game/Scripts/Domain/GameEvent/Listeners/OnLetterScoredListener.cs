using System;
using System.Linq;

public class OnLetterScoredListener : BaseGameEventListener<Func<Level, Letter, bool>, LetterScoredDelegate>
{
    public void OnLetterScored(ref int points, ref int multiplier, Level level, Letter letter)
    {
        if (Validators.Any(validator => !validator(level, letter)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier(ref points, ref multiplier);
        }
    }
}

public delegate void LetterScoredDelegate(ref int points, ref int multiplier);