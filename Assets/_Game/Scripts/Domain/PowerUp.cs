using System;

[Serializable]
public class PowerUp
{
    private PowerUpType Type { get; set; }
    public int Uses { get; private set; }

    public PowerUp(PowerUpType type, int uses)
    {
        Type = type;
        Uses = uses;
    }

    public bool TryUse()
    {
        if (Uses <= 0)
            return false;

        Uses--;
        return true;
    }

    public void Gain(int quantity = 1)
    {
        Uses += quantity;
    }
}

public enum PowerUpType
{
    Troca,
    Bomba,
    Misturar
}