using System;
using System.Collections.Generic;
using System.Linq;

public class OnPowerUpUsedListener : BaseGameEventListener<Func<PowerUp, List<LetterController>, bool>, PowerUpUsedDelegate>
{
    public void OnBoardShuffled(PowerUp powerUp, List<LetterController> letters)
    {
        if (Validators.Any(validator => !validator(powerUp, letters)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier(powerUp, letters);
        }
    }
}

public delegate void PowerUpUsedDelegate(PowerUp powerUp, List<LetterController> letters);
