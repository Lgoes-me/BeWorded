using System;
using System.Collections.Generic;
using System.Linq;

public class OnPowerUpUsedListener : BaseGameEventListener
{
    private List<Func<PowerUp, List<LetterController>, bool>> Validators { get; set; }
    private List<PowerUpUsedDelegate> Modifiers { get; set; }
    
    internal OnPowerUpUsedListener(
        List<Func<PowerUp, List<LetterController>, bool>> validators, 
        List<PowerUpUsedDelegate> modifiers)
    {
        Validators = validators;
        Modifiers = modifiers;
    }

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
