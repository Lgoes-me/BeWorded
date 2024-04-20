using System;
using System.Linq;

public class OnMatchStartedListener : BaseGameEventListener<Func<bool>, MatchStartedDelegate>
{
    public void OnMatchStarted()
    {
        if (Validators.Any(validator => !validator()))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier();
        }
    }
}

public delegate void MatchStartedDelegate();