using System;
using System.Collections.Generic;
using System.Linq;

public class OnPowerUpUsedListener : BaseGameEventListener<Func<PowerUp, LetterController[], bool>, PowerUpUsedDelegate>
{
    public void OnPowerUpUsed(PowerUp powerUp, params LetterController[] letters)
    {
        if (Validators.Any(validator => !validator(powerUp, letters)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier(powerUp, letters);
        }
    }
}

public delegate void PowerUpUsedDelegate(PowerUp powerUp,params LetterController[] letters);
