using System;
using System.Collections.Generic;

public interface IOnLetterPrizeCreditedJoker : IJoker
{
    void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter);
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
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
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
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Characters.Contains(letter.Character))
            return;
        
        baseMultiplier += ExtraMultiplier;
    }
}

public class MoneyPerCharacterCreditedJoker : IOnLetterPrizeCreditedJoker
{
    private Player Player { get; set; }
    private List<char> Characters { get; set; }
    private int Money { get; set; }
    
    public MoneyPerCharacterCreditedJoker(Player player, List<char> characters, int money)
    {
        Player = player;
        Characters = characters;
        Money = money;
    }
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Characters.Contains(letter.Character))
            return;
        
        Player.GivePrize(Money);
    }
}

public class PowerUpPerCharacterCreditedJoker : IOnLetterPrizeCreditedJoker
{
    private Player Player { get; set; }
    private List<char> Characters { get; set; }
    private PowerUpType PowerUpType { get; set; }
    private int Quantity { get; set; }
    
    public PowerUpPerCharacterCreditedJoker(Player player, List<char> characters, PowerUpType powerUpType, int quantity = 1)
    {
        Player = player;
        Characters = characters;
        PowerUpType = powerUpType;
        Quantity = quantity;
    }
    
    public void OnLetterPrizeCredited(ref int basePrice, ref int baseMultiplier, Letter letter)
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