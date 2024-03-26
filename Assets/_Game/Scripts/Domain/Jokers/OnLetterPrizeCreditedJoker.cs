using System;
using System.Collections.Generic;

public abstract class BaseOnLetterPrizeCreditedJoker : BaseJoker
{
    public abstract void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter);

    protected BaseOnLetterPrizeCreditedJoker(string id) : base(id)
    {
        
    }
}

public class ExtraPrizeCreditedPerCharacterJoker : BaseOnLetterPrizeCreditedJoker
{
    private List<char> Characters { get; set; }
    private int ExtraPrize { get; set; }

    public ExtraPrizeCreditedPerCharacterJoker(string id, List<char> characters, int extraPrize) : base(id)
    {
        Characters = characters;
        ExtraPrize = extraPrize;
    }
    
    public override void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Characters.Contains(letter.Character))
            return;

        basePrice += ExtraPrize;
    }
}

public class ExtraMultiplierPerCharacterCreditedJoker : BaseOnLetterPrizeCreditedJoker
{
    private List<char> Characters { get; set; }
    private int ExtraMultiplier { get; set; }
    
    public ExtraMultiplierPerCharacterCreditedJoker(string id, List<char> characters, int extraMultiplier) : base(id)
    {
        Characters = characters;
        ExtraMultiplier = extraMultiplier;
    }
    
    public override void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Characters.Contains(letter.Character))
            return;
        
        baseMultiplier += ExtraMultiplier;
    }
}

public class MoneyPerCharacterCreditedJoker : BaseOnLetterPrizeCreditedJoker
{
    private Player Player { get; set; }
    private List<char> Characters { get; set; }
    private int Money { get; set; }
    
    public MoneyPerCharacterCreditedJoker(string id, Player player, List<char> characters, int money) : base(id)
    {
        Player = player;
        Characters = characters;
        Money = money;
    }
    
    public override void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Characters.Contains(letter.Character))
            return;
        
        Player.GivePrize(Money);
    }
}

public class PowerUpPerCharacterCreditedJoker : BaseOnLetterPrizeCreditedJoker
{
    private Player Player { get; set; }
    private List<char> Characters { get; set; }
    private PowerUpType PowerUpType { get; set; }
    private int Quantity { get; set; }
    
    public PowerUpPerCharacterCreditedJoker
        (string id, Player player, List<char> characters, PowerUpType powerUpType, int quantity = 1) : base(id)
    {
        Player = player;
        Characters = characters;
        PowerUpType = powerUpType;
        Quantity = quantity;
    }
    
    public override void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Characters.Contains(letter.Character))
            return;

        switch (PowerUpType)
        {
            case PowerUpType.Troca:
                Player.Swaps.Gain(Quantity);
                break;
            case PowerUpType.Bomba:
                Player.Bombs.Gain(Quantity);
                break;
            case PowerUpType.Misturar:
                Player.Shuffles.Gain(Quantity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}