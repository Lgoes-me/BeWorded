using System.Collections.Generic;

public abstract class PowerUpValidator
{
    public abstract bool Validate(PowerUp powerUp, List<Letter> letters);
}