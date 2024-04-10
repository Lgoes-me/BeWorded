using System.Collections.Generic;

public abstract class PowerUpModifier
{
    public abstract void DoModification(PowerUp powerUp, List<LetterController> letters);
}