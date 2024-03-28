using System;

[Serializable]
public class PowerUp
{
    public PowerUpType Type { get; private set; }
    public int Uses { get; private set; }

    public PowerUp(PowerUpType type, int uses)
    {
        Type = type;
        Uses = uses;
    }

    public bool CanUse()
    {
        return Uses > 0;
    }

    public void Use()
    {
        Uses--;
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