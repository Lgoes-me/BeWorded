public class PowerUp
{
    private PowerUpType Type  { get; set; }
    public int Uses  { get; private set; }

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

    public void Gain()
    {
        Uses++;
    }
}

public enum PowerUpType
{
    Troca,
    Bomba,
    Misturar
}