using System;
using System.Collections.Generic;

public interface IOnLetterPrizeCreditedJoker : IJoker
{
    void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter, Player player);
}

public class ExtraPrizeCreditedPerCharacterJoker : IOnLetterPrizeCreditedJoker
{
    private List<char> Characters { get; set; }
    private int ExtraPrize { get; set; }
    
    public ExtraPrizeCreditedPerCharacterJoker(List<char> characters, int extraPrize)
    {
        Characters = characters;
        ExtraPrize = extraPrize;
    }
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter, Player player)
    {
        if (!Characters.Contains(letter.Character))
            return;

        basePrice += ExtraPrize;
    }
}

public class ExtraMultiplierPerCharacterCreditedJoker : IOnLetterPrizeCreditedJoker
{
    private List<char> Characters { get; set; }
    private int ExtraMultiplier { get; set; }
    
    public ExtraMultiplierPerCharacterCreditedJoker(List<char> characters, int extraMultiplier)
    {
        Characters = characters;
        ExtraMultiplier = extraMultiplier;
    }
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter, Player player)
    {
        if (!Characters.Contains(letter.Character))
            return;
        
        baseMultiplier += ExtraMultiplier;
    }
}

public class MoneyPerCharacterCreditedJoker : IOnLetterPrizeCreditedJoker
{
    private List<char> Characters { get; set; }
    private int Money { get; set; }
    
    public MoneyPerCharacterCreditedJoker(List<char> characters, int money)
    {
        Characters = characters;
        Money = money;
    }
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter, Player player)
    {
        if (!Characters.Contains(letter.Character))
            return;
        
        player.GivePrize(Money);
    }
}

public class PowerUpPerCharacterCreditedJoker : IOnLetterPrizeCreditedJoker
{
    private List<char> Characters { get; set; }
    private PowerUpType PowerUpType { get; set; }
    private int Quantity { get; set; }
    
    public PowerUpPerCharacterCreditedJoker(List<char> characters, PowerUpType powerUpType, int quantity = 1)
    {
        Characters = characters;
        PowerUpType = powerUpType;
        Quantity = quantity;
    }
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter, Player player)
    {
        if (!Characters.Contains(letter.Character))
            return;

        switch (PowerUpType)
        {
            case PowerUpType.Troca:
                player.Swaps.Gain(Quantity);
                break;
            case PowerUpType.Bomba:
                player.Bombs.Gain(Quantity);
                break;
            case PowerUpType.Misturar:
                player.Shuffles.Gain(Quantity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}